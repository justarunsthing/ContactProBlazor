using ContactProBlazor.Models;
using ContactProBlazor.Interfaces;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Services
{
    public class CategoryDTOService(ICategoryRepository repository) : ICategoryDTOService
    {
        public async Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId)
        {
            Category newCategory = new()
            {
                AppUserId = userId,
                Name = category.Name
            };

            newCategory = await repository.CreateCategoryAsync(newCategory);

            return newCategory.ToDTO();
        }
    }
}