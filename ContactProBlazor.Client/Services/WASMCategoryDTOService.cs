using System.Net.Http.Json;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Client.Services
{
    public class WASMCategoryDTOService(HttpClient http) : ICategoryDTOService
    {
        public Task<CategoryDTO> GetCategoryByIdAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CategoryDTO>> GetCategoriesAsync(string userId)
        {
            return await http.GetFromJsonAsync<List<CategoryDTO>>("api/categories") ?? [];
        }

        public Task<CategoryDTO> CreateCategoryAsync(CategoryDTO category, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateCategoryAsync(CategoryDTO category, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteCategoryAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }
    }
}