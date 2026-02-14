using ContactProBlazor.Models;

namespace ContactProBlazor.Interfaces
{
    public interface IContactRepository
    {
        Task<Contact> CreateContactAsync(Contact contact);
        Task AddCategoriesToContactAsync(int contactId, string userId, List<int> categoryIds);
    }
}