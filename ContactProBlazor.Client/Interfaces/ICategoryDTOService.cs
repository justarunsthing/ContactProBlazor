using ContactProBlazor.Client.Models;

namespace ContactProBlazor.Client.Interfaces
{
    public interface ICategoryDTOService
    {
        Task<CategoryDTO> GetCategoryByIdAsync(int id, string userId);
        Task<List<CategoryDTO>> GetCategoriesAsync(string userId);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId);
        Task UpdateCategoryAsync(CategoryDTO category, string userId);
    }
}