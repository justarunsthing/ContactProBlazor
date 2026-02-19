using ContactProBlazor.Data;
using ContactProBlazor.Models;
using ContactProBlazor.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace ContactProBlazor.Services
{
    public class ContactRepository(IDbContextFactory<ApplicationDbContext> contextFactory) : IContactRepository
    {
        public async Task<Contact?> GetContactByIdAsync(int id, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Contact? contact = await context.Contacts
                                            .Include(c => c.Categories)
                                            .FirstOrDefaultAsync(c => c.Id == id && c.AppUserId == userId);

            return contact;
        }

        public async Task<List<Contact>> GetContactsAsync(string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            List<Contact> contacts = await context.Contacts
                                                  .Where(c => c.AppUserId == userId)
                                                  .Include(c => c.Categories)
                                                  .ToListAsync();

            return contacts;
        }

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

        public async Task UpdateContactAsync(Contact contact)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            if (await context.Contacts.AnyAsync(c => c.Id == contact.Id && c.AppUserId == contact.AppUserId))
            {
                ImageUpload? oldImage = null;

                // User changed image
                if (contact.Image != null)
                {
                    if (contact.Image.Id != contact.ImageId)
                    {
                        // Find the old image
                        oldImage = await context.Images.FirstOrDefaultAsync(img => img.Id == contact.ImageId);
                    }

                    // Save the new image
                    contact.ImageId = contact.Image.Id;
                    context.Images.Add(contact.Image);
                }

                context.Contacts.Update(contact);
                await context.SaveChangesAsync();

                if (oldImage != null)
                {
                    context.Images.Remove(oldImage);
                    await context.SaveChangesAsync();
                }
            }
        }

        public async Task RemoveCategoriesFromContactAsync(int contactId, string userId)
        {
            using ApplicationDbContext context = contextFactory.CreateDbContext();

            Contact? contact = await context.Contacts
                                            .Include(c => c.Categories)
                                            .FirstOrDefaultAsync(c => c.Id == contactId && c.AppUserId == userId);

            if (contact != null)
            {
                contact.Categories.Clear();
                await context.SaveChangesAsync();
            }
        }
    }
}