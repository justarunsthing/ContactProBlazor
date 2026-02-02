using ContactProBlazor.Models;
using ContactProBlazor.Interfaces;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Services
{
    public class CategoryDTOService(ICategoryRepository repository) : ICategoryDTOService
    {
        public async Task<CategoryDTO> GetCategoryByIdAsync(int id, string userId)
        {
            Category? category = await repository.GetCategoryAsync(id, userId);

            return category?.ToDTO();
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync(string userId)
        {
            List<Category> categories = await repository.GetCategoriesAsync(userId);

            return categories.Select(c => c.ToDTO()).ToList();
        }

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

        public async Task UpdateCategoryAsync(CategoryDTO category, string userId)
        {
            Category? categoryToUpdate = await repository.GetCategoryAsync(category.Id, userId);

            if (categoryToUpdate != null)
            {
                categoryToUpdate.Name = category.Name;

                // Prevents duplicate db records
                categoryToUpdate.Contacts.Clear();

                await repository.UpdateCategoryAsync(categoryToUpdate, userId);
            }
        }
    }
}