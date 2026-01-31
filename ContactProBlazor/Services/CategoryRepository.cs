using ContactProBlazor.Data;
using ContactProBlazor.Models;
using ContactProBlazor.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactProBlazor.Services
{
    public class CategoryRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : ICategoryRepository
    {
        public async Task<List<Category>> GetCategoriesAsync(string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            List<Category> categories = await context.Categories
                                                     .Where(c => c.AppUserId == userId)
                                                     .Include(c => c.Contacts)
                                                     .ToListAsync();

            return categories;
        }

        public async Task<Category> CreateCategoryAsync(Category category)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();
            context.Categories.Add(category);

            await context.SaveChangesAsync();

            return category;
        }
    }
}