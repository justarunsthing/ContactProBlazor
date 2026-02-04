using ContactProBlazor.Models;

namespace ContactProBlazor.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category?> GetCategoryAsync(int id, string userId);
        Task<List<Category>> GetCategoriesAsync(string userId);
        Task<Category> CreateCategoryAsync(Category category);
        Task UpdateCategoryAsync(Category category, string userId);
        Task DeleteCategoryAsync(int id, string userId);
    }
}