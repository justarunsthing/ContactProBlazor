using ContactProBlazor.Data;
using ContactProBlazor.Models;
using ContactProBlazor.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactProBlazor.Services
{
    public class CategoryRepository : ICategoryRepository
    {
        public CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory)
        {
            
        }

        public Task<Category> CreateCategoryAsync(Category category)
        {
            throw new NotImplementedException();
        }
    }
}