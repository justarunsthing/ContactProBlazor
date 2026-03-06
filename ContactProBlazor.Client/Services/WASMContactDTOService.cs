using System.Net.Http.Json;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Client.Services
{
    public class WASMContactDTOService(HttpClient http) : IContactDTOService
    {
        public async Task<ContactDTO?> GetContactByIdAsync(int id, string userId)
        {
            try
            {
                return await http.GetFromJsonAsync<ContactDTO>($"api/contacts/{id}");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return null;
            }
        }

        public async Task<List<ContactDTO>> GetContactsAsync(string userId)
        {
            return await http.GetFromJsonAsync<List<ContactDTO>>("api/contacts") ?? [];
        }

        public async Task<List<ContactDTO>> GetContactsByCategoryAsync(int categoryId, string userId)
        {
            return await http.GetFromJsonAsync<List<ContactDTO>>($"api/contacts?categoryId={categoryId}") ?? [];
        }

        public async Task<List<ContactDTO>> SearchContactsAsync(string searchTerm, string userId)
        {
            return await http.GetFromJsonAsync<List<ContactDTO>>($"api/contacts/search?searchTerm={searchTerm}") ?? [];
        }

        public async Task<ContactDTO> CreateContactAsync(ContactDTO dto, string userId)
        {
            HttpResponseMessage response = await http.PostAsJsonAsync($"api/contacts", dto);
            response.EnsureSuccessStatusCode();

            ContactDTO? createdContact = await response.Content.ReadFromJsonAsync<ContactDTO>();

            return createdContact ?? throw new HttpRequestException("Invalid JSON response from endpoint");
        }

        public async Task UpdateContactAsync(ContactDTO dto, string userId)
        {
            HttpResponseMessage response = await http.PutAsJsonAsync($"api/contacts/{dto.Id}", dto);
            response.EnsureSuccessStatusCode();
        }

        public async Task DeleteContactAsync(int id, string userId)
        {
            HttpResponseMessage response = await http.DeleteAsync($"api/contacts/{id}");
            response.EnsureSuccessStatusCode();
        }

        public async Task<bool> EmailContactAsync(int id, EmailData emailData, string userId)
        {
            try
            {
                HttpResponseMessage response = await http.PostAsJsonAsync($"api/contacts/email/{id}", emailData);

                return response.IsSuccessStatusCode;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                return false;
            }
        }
    }
}