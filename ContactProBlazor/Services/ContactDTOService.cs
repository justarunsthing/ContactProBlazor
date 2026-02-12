using ContactProBlazor.Interfaces;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Services
{
    public class ContactDTOService(IContactRepository repository) : IContactDTOService
    {
        public Task<ContactDTO> CreateContactAsync(ContactDTO contact, string userId)
        {
            throw new NotImplementedException();
        }
    }
}