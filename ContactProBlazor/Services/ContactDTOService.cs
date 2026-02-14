using ContactProBlazor.Models;
using ContactProBlazor.Helpers;
using ContactProBlazor.Interfaces;
using ContactProBlazor.Client.Models;
using ContactProBlazor.Client.Interfaces;

namespace ContactProBlazor.Services
{
    public class ContactDTOService(IContactRepository repository) : IContactDTOService
    {
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
    }
}