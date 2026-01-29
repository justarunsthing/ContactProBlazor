using ContactProBlazor.Client.Models;

namespace ContactProBlazor.Client.Interfaces
{
    public interface ICategoryDTOService
    {
        Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId);
    }
}