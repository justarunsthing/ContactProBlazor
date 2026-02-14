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

        public async Task AddCategoriesToContactAsync(int contactId, string userId, List<int> categoryIds)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Contact? contact = await context.Contacts
                                            .Include(c => c.Categories)
                                            .FirstOrDefaultAsync(c => c.Id == contactId && c.AppUserId == userId);

            if (contact != null)
            {
                foreach (var categoryId in categoryIds)
                {
                    Category? category = await context.Categories
                                                      .Include(c => c.Contacts)
                                                      .FirstOrDefaultAsync(c => c.Id == categoryId && c.AppUserId == userId);

                    if (category != null)
                    {
                        contact.Categories.Add(category);
                    }
                }

                await context.SaveChangesAsync();
            }
        }
    }
}