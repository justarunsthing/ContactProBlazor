using ContactProBlazor.Models;
using ContactProBlazor.Helpers;
using ContactProBlazor.Interfaces;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Services
{
    public class ContactDTOService(IContactRepository repository) : IContactDTOService
    {
        public async Task<ContactDTO?> GetContactByIdAsync(int id, string userId)
        {
            Contact? contact = await repository.GetContactByIdAsync(id, userId);

            return contact?.ToDTO();
        }

        public async Task<List<ContactDTO>> GetContactsAsync(string userId)
        {
            List<Contact> contacts = await repository.GetContactsAsync(userId);
            List<ContactDTO> dtos = [.. contacts.Select(c => c.ToDTO())];

            return dtos;
        }

        public async Task<ContactDTO> CreateContactAsync(ContactDTO dto, string userId)
        {
            Contact newContact = new()
            {
                AppUserId = userId,
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                BirthDate = dto.BirthDate,
                Address1 = dto.Address1,
                Address2 = dto.Address2,
                City = dto.City,
                PostCode = dto.PostCode,
                Email = dto.Email,
                PhoneNumber = dto.PhoneNumber,
                Created = DateTime.UtcNow
            };

            // Save image, convert URL to the ImageUpload type
            if (dto.ProfileImageUrl?.StartsWith("data:") == true)
            {
                newContact.Image = ImageHelper.GetImageUploadFromUrl(dto.ProfileImageUrl);
            }

            newContact = await repository.CreateContactAsync(newContact);

            // Add categories to the contact
            List<int> categoryIds = dto.Categories?.Select(c => c.Id).ToList() ?? [];

            await repository.AddCategoriesToContactAsync(newContact.Id, userId, categoryIds);

            // Requery to get the updated contact, override
            newContact = (await repository.GetContactByIdAsync(newContact.Id, userId))!;

            return newContact.ToDTO();
        }

        public async Task UpdateContactAsync(ContactDTO dto, string userId)
        {
            Contact? contact = await repository.GetContactByIdAsync(dto.Id, userId);

            if (contact != null)
            {
                contact.FirstName = dto.FirstName;
                contact.LastName = dto.LastName;
                contact.BirthDate = dto.BirthDate;
                contact.Address1 = dto.Address1;
                contact.Address2 = dto.Address2;
                contact.City = dto.City;
                contact.PostCode = dto.PostCode;
                contact.Email = dto.Email;
                contact.PhoneNumber = dto.PhoneNumber;

                // User updated image
                if (dto.ProfileImageUrl?.StartsWith("data:") == true)
                {
                    contact.Image = ImageHelper.GetImageUploadFromUrl(dto.ProfileImageUrl);
                }
                else
                {
                    contact.Image = null;
                }

                // Clear existing categories and update with new ones, prevent duplicates and new insertions
                contact.Categories.Clear();
                // Update contact first to ensure the contact is saved before updating categories
                await repository.UpdateContactAsync(contact);
                // Remove existing categories
                await repository.RemoveCategoriesFromContactAsync(contact.Id, userId);

                // Add new categories
                List<int> categoryIds = dto.Categories?.Select(c => c.Id).ToList() ?? [];
                await repository.AddCategoriesToContactAsync(contact.Id, userId, categoryIds);
            }
        }

        public async Task DeleteContactAsync(int id, string userId)
        {
            await repository.DeleteContactAsync(id, userId);
        }
    }
}