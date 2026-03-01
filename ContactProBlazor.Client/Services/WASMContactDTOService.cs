using System.Net.Http.Json;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Client.Services
{
    public class WASMContactDTOService(HttpClient http) : IContactDTOService
    {
        public Task<ContactDTO?> GetContactByIdAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public async Task<List<ContactDTO>> GetContactsAsync(string userId)
        {
            return await http.GetFromJsonAsync<List<ContactDTO>>("api/contacts") ?? [];
        }

        public async Task<List<ContactDTO>> GetContactsByCategoryAsync(int categoryId, string userId)
        {
            return await http.GetFromJsonAsync<List<ContactDTO>>($"api/contacts?categoryId={categoryId}") ?? [];
        }

        public Task<List<ContactDTO>> SearchContactsAsync(string searchTerm, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<ContactDTO> CreateContactAsync(ContactDTO dto, string userId)
        {
            throw new NotImplementedException();
        }

        public Task UpdateContactAsync(ContactDTO dto, string userId)
        {
            throw new NotImplementedException();
        }

        public Task DeleteContactAsync(int id, string userId)
        {
            throw new NotImplementedException();
        }

        public Task<bool> EmailContactAsync(int id, EmailData emailData, string userId)
        {
            throw new NotImplementedException();
        }
    }
}