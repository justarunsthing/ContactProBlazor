using ContactProBlazor.Models;

namespace ContactProBlazor.Interfaces
{
    public interface ICategoryRepository
    {
        Task<List<Category>> GetCategoriesAsync(string userId);
        Task<Category> CreateCategoryAsync(Category category);
    }
}