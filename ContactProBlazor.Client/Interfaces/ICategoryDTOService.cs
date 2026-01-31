using ContactProBlazor.Client.Models;

namespace ContactProBlazor.Client.Interfaces
{
    public interface ICategoryDTOService
    {
        Task<List<CategoryDTO>> GetCategoriesAsync(string userId);
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId);
    }
}