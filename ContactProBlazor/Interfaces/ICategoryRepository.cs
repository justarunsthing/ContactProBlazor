using ContactProBlazor.Models;

namespace ContactProBlazor.Interfaces
{
    public interface ICategoryRepository
    {
        Task<Category> CreateCategoryAsync(Category category);
    }
}