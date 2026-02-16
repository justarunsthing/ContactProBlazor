using ContactProBlazor.Client.Models;

namespace ContactProBlazor.Client.Interfaces
{
    public interface IContactDTOService
    {
        Task<List<ContactDTO>> GetContactsAsync(string userId);
        Task<ContactDTO> CreateContactAsync(ContactDTO dto, string userId);
    }
}