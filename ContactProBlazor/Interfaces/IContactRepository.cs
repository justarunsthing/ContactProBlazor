using ContactProBlazor.Models;

namespace ContactProBlazor.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> CreateContactAsync(Contact contact);
    }
}