using ContactProBlazor.Models;
using ContactProBlazor.Interfaces;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace ContactProBlazor.Services
{
    public class CategoryDTOService(ICategoryRepository repository, IEmailSender emailSender) : ICategoryDTOService
    {
        public async Task<CategoryDTO?> GetCategoryByIdAsync(int id, string userId)
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

        public async Task DeleteCategoryAsync(int id, string userId)
        {
            await repository.DeleteCategoryAsync(id, userId);
        }

        public async Task<bool> EmailCategoryAsync(int id, EmailData emailData, string userId)
        {
            Category? category = await repository.GetCategoryAsync(id, userId);

            if (category == null || category.Contacts.Count < 1)
            {
                return false;
            }

            try
            {
                string recipients = string.Join(";", category.Contacts.Select(c => c.Email));

                await emailSender.SendEmailAsync(recipients, emailData.Subject, emailData.Body);

                return true;
            }
            catch
            {
                return false;
            }
        }
    }
}