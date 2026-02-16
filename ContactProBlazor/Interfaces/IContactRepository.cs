using ContactProBlazor.Models;

namespace ContactProBlazor.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact?> GetContactByIdAsync(int id, string userId);
        Task<List<Contact>> GetContactsAsync(string userId);
        Task<Contact> CreateContactAsync(Contact contact);
        Task AddCategoriesToContactAsync(int contactId, string userId, List<int> categoryIds);
    }
}