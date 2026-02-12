using ContactProBlazor.Client.Models;

namespace ContactProBlazor.Client.Interfaces
{
    public interface IContactDTOService
    {
        Task<ContactDTO> CreateContactAsync(ContactDTO dto, string userId);
    }
}