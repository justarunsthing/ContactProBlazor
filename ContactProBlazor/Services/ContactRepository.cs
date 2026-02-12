using ContactProBlazor.Data;
using ContactProBlazor.Models;
using ContactProBlazor.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactProBlazor.Services
{
    public class ContactRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IContactRepository
    {
        public Task<Contact> CreateContactAsync(Contact contact)
        {
            throw new NotImplementedException();
        }
    }
}