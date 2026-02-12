using ContactProBlazor.Data;
using ContactProBlazor.Models;
using ContactProBlazor.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactProBlazor.Services
{
    public class ContactRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IContactRepository
    {
        public async Task<Contact> CreateContactAsync(Contact contact)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();
            context.Contacts.Add(contact);

            await context.SaveChangesAsync();

            return contact;
        }
    }
}