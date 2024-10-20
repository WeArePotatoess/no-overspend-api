using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using No_Overspend_Api.Base;
using No_Overspend_Api.DTOs.Base;
using No_Overspend_Api.DTOs.Category.Request;
using No_Overspend_Api.DTOs.Category.Response;
using No_Overspend_Api.HttpExceptions;
using No_Overspend_Api.Infra.Constants;
using No_Overspend_Api.Infra.Models;
using System.Text.Json;

namespace No_Overspend_Api.Services
{
    public interface ICategoryService
    {
        public Task<PagedResponse<CategoryView>> SearchAsync(string userId, CategoryFilter request);
        public Task<CategoryView> GetDetailAsync(string userId, GetDetailRequest request);
        public Task<bool> UpdateAsync(string userId, UpdateCategoryRequest request);
        public Task<bool> DeleteAsync(string userId, DeleteRequest request);
        public Task<bool> InitDataAsync(string userId);
        public Task<string> CreateAsync(string userId, CreateCategoryRequest request);
    }
    public class CategoryService : ICategoryService
    {
        private readonly NoOverspendContext _context;
        private readonly IMapper _mapper;
        public CategoryService(NoOverspendContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<string> CreateAsync(string userId, CreateCategoryRequest request)
        {
            var existed = await _context.categories
                .AsNoTracking()
                .AnyAsync(e => e.user_id == userId && e.name == request.name);
            if (existed) throw new BadRequestException(ErrorMessages.CategoryExisted);
            var category = _mapper.Map<category>(request);
            _context.categories.Add(category);
            await _context.SaveChangesAsync();
            return category.id;
        }

        public async Task<bool> DeleteAsync(string userId, DeleteRequest request)
        {
            var category = await _context.categories
                .Where(e => e.user_id == userId && e.id == request.id)
                .FirstOrDefaultAsync();
            if (category == null) throw new NotFoundException(ErrorMessages.NotFound);
            category.SoftRemove();
            await _context.SaveChangesAsync();
            return true;
        }

        public async Task<CategoryView> GetDetailAsync(string userId, GetDetailRequest request)
        {
            var category = await _context.categories
                .AsNoTracking()
                .Include(e => e.icon)
                .FirstOrDefaultAsync(e => e.user_id == userId && e.id == request.id);
            if (category == null) throw new NotFoundException(ErrorMessages.NotFound);
            return _mapper.Map<CategoryView>(category);
        }

        public async Task<bool> InitDataAsync(string userId)
        {
            var jsonString = File.ReadAllText(Path.Combine(StaticVariables.BasePath, @"InitDatas\Categories.json"));
            var jsonData = JsonSerializer.Deserialize<List<category>>(jsonString);
            if (jsonData != null && jsonData.Any())
            {
                _context.categories.AddRange(jsonData);
                await _context.SaveChangesAsync();
            }
            return true;
        }

        public async Task<PagedResponse<CategoryView>> SearchAsync(string userId, CategoryFilter request)
        {
            var categories = _context.categories
                .AsNoTracking()
                .Where(e => request.keyword == null || e.name.ToLower().Trim().Contains(request.keyword))
                .Include(e => e.icon)
                .OrderByDescending(e => e.created_at)
                .Select(e => _mapper.Map<CategoryView>(e));
            var result = await categories
                .Paged(request.page_index, request.page_size)
                .ToListAsync();
            return new PagedResponse<CategoryView>
            {
                Items = result,
                keyword = request.keyword,
                page_index = request.page_index,
                page_size = request.page_size,
                total_items = categories.Count(),
            };
        }

        public async Task<bool> UpdateAsync(string userId, UpdateCategoryRequest request)
        {
            var category = await _context.categories
                .FirstOrDefaultAsync(e => e.user_id == userId && e.id == request.id);
            if (category == null) throw new NotFoundException(ErrorMessages.NotFound);
            category.name = request.name;
            category.description = request.description;
            category.icon_id = request.icon_id ?? StaticVariables.DefaultIconId;
            category.Updated();
            await _context.SaveChangesAsync();
            return true;
        }
    }
}
