using ContactProBlazor.Client.Models;
using ContactProBlazor.Data;
using System.ComponentModel.DataAnnotations;

namespace ContactProBlazor.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }

        [Required]
        public string? AppUserId { get; set; }
        public virtual ApplicationUser? AppUser { get; set; }
        public virtual ICollection<Contact>? Contacts { get; set; } = [];

        public CategoryDTO ToDTO()
        {
            var dto = new CategoryDTO
            {
                Id = this.Id,
                Name = this.Name
            };

            foreach (var contact in Contacts ?? [])
            {
                // Prevent circular reference
                contact.Categories.Clear();
                dto.Contacts?.Add(contact.ToDTO());
            }

            return dto;
        }
    }
}