using AutoMapper;
using Microsoft.EntityFrameworkCore;
using No_Overspend_Api.Base;
using No_Overspend_Api.DTOs.Base;
using No_Overspend_Api.DTOs.Transaction.Request;
using No_Overspend_Api.DTOs.Transaction.Response;
using No_Overspend_Api.HttpExceptions;
using No_Overspend_Api.Infra.Constants;
using No_Overspend_Api.Infra.Enums;
using No_Overspend_Api.Infra.Models;

namespace No_Overspend_Api.Services
{
    public interface ITransactionService
    {
        public Task<ExtendedPageResponse<TransactionView>> SearchAsync(string userId, TransactionFilter request);
        public Task<TransactionView> GetDetailAsync(string userId, GetDetailRequest request);
        public Task<string> CreateAsync(string userId, CreateTransactionRequest request);
        public Task<bool> DeleteAsync(string userId, DeleteRequest request);
        public Task<bool> UpdateAsync(string userId, UpdateTransactionRequest request);
    }
    public class TransactionService : ITransactionService
    {
        private readonly IMapper _mapper;
        private readonly NoOverspendContext _context;
        private readonly IBalanceService _balanceService;
        public TransactionService(NoOverspendContext context, IMapper mapper, IBalanceService balanceService)
        {
            _context = context;
            _mapper = mapper;
            _balanceService = balanceService;
        }

        public async Task<string> CreateAsync(string userId, CreateTransactionRequest request)
        {
            using (var transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var newTransaction = _mapper.Map<transaction>(request);
                    if (newTransaction.type == (int)eType_Transaction.Expense)
                    {
                        newTransaction.amount = -newTransaction.amount;
                    }
                    await _balanceService.UpdateBalance(userId, newTransaction.amount);
                    _context.transactions.Add(newTransaction);
                    await _context.SaveChangesAsync();
                    return newTransaction.id;
                }
                catch (Exception)
                {
                    await transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<bool> DeleteAsync(string userId, DeleteRequest request)
        {
            using (var _transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var transaction = await _context.transactions
                        .FirstOrDefaultAsync(e => e.user_id == userId && e.id == request.id);
                    if (transaction == null) throw new NotFoundException(ErrorMessages.NotFound);
                    transaction.SoftRemove();
                    await _context.SaveChangesAsync();
                    await _balanceService.UpdateBalance(userId, -transaction.amount);
                    await _transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
        }

        public async Task<TransactionView> GetDetailAsync(string userId, GetDetailRequest request)
        {
            var transaction = await _context.transactions
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.user_id == userId && e.id == request.id);
            if (transaction == null) throw new NotFoundException(ErrorMessages.NotFound);
            var response = _mapper.Map<TransactionView>(transaction);
            return response;
        }

        public async Task<ExtendedPageResponse<TransactionView>> SearchAsync(string userId, TransactionFilter request)
        {
            var transactions = _context.transactions
                .AsNoTracking()
                .Include(e => e.category)
                .OrderByDescending(e => e.transaction_date)
                .Where(e => request.from_date == null || e.transaction_date >= request.from_date)
                .Where(e => request.to_date == null || e.transaction_date <= request.to_date)
                .Where(e => request.keyword == null || e.title.ToLower().Trim().Contains(request.keyword) || (e.message != null && e.message.ToLower().Trim().Contains(request.keyword)))
                .Where(e => request.type == null || e.type == (int)request.type)
                .Select(e => _mapper.Map<TransactionView>(e));
            var result = await transactions
                .Paged(request.page_index, request.page_size)
                .ToListAsync();
            return new ExtendedPageResponse<TransactionView>
            {
                Items = result,
                keyword = request.keyword,
                page_index = request.page_index,
                page_size = request.page_size,
                total_items = transactions.Count(),
                metadata = new
                {
                    total_income = await transactions.Where(e => e.type == eType_Transaction.Income).SumAsync(e => e.amount),
                    total_expense = await transactions.Where(e => e.type == eType_Transaction.Expense).SumAsync(e => e.amount),
                }
            };
        }

        public async Task<bool> UpdateAsync(string userId, UpdateTransactionRequest request)
        {
            using (var _transaction = await _context.Database.BeginTransactionAsync())
            {
                try
                {
                    var transaction = await _context.transactions
                        .FirstOrDefaultAsync(e => e.user_id == userId && e.id == request.id);
                    if (transaction == null) throw new NotFoundException(ErrorMessages.NotFound);

                    var differenceAmount = request.amount - transaction.amount;
                    transaction.message = request.message;
                    transaction.title = request.title;
                    transaction.amount = request.amount;
                    transaction.category_id = request.category_id;
                    transaction.Updated();
                    await _balanceService.UpdateBalance(userId, differenceAmount);
                    await _context.SaveChangesAsync();
                    await _transaction.CommitAsync();
                    return true;
                }
                catch (Exception)
                {
                    await _transaction.RollbackAsync();
                    throw;
                }
            }
        }
    }
}
