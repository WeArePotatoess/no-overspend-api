using Microsoft.EntityFrameworkCore;
using No_Overspend_Api.HttpExceptions;
using No_Overspend_Api.Infra.Constants;
using No_Overspend_Api.Infra.Models;

namespace No_Overspend_Api.Services
{
    public interface IBalanceService
    {
        public Task<bool> UpdateBalance(string userId, decimal amount);
        public Task<decimal> GetBalance(string userId);

    }
    public class BalanceService : IBalanceService
    {
        private readonly NoOverspendContext _context;
        public BalanceService(NoOverspendContext context)
        {
            _context = context;
        }

        public async Task<decimal> GetBalance(string userId)
        {
            var user = await _context.users
                .AsNoTracking()
                .FirstOrDefaultAsync(e => e.id == userId);
            if (user == null) throw new NotFoundException(ErrorMessages.NotFound);
            return user.total_balance;
        }

        public async Task<bool> UpdateBalance(string userId, decimal amount)
        {
            var user = await _context.users
                .FirstOrDefaultAsync(e => e.id == userId);
            if (user == null) throw new NotFoundException(ErrorMessages.NotFound);
            user.total_balance += amount;
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
