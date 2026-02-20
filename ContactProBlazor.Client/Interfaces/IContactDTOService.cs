using ContactProBlazor.Client.Models;

namespace ContactProBlazor.Client.Interfaces
{
    public interface IContactDTOService
    {
        Task<ContactDTO?> GetContactByIdAsync(int id, string userId);
        Task<List<ContactDTO>> GetContactsAsync(string userId);
        Task<ContactDTO> CreateContactAsync(ContactDTO dto, string userId);
        Task UpdateContactAsync(ContactDTO dto, string userId);
        Task DeleteContactAsync(int id, string userId);
    }
}