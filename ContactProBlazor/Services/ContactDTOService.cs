using ContactProBlazor.Models;
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

            newContact = await repository.CreateContactAsync(newContact);

            return newContact.ToDTO();
        }
    }
}