using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ContactProBlazor.Models
{
    public class Contact
    {
        private DateTimeOffset _created;
        private DateTimeOffset? _birthDate;

        public int Id { get; set; }

        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and a max {1} characters long", MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and a max {1} characters long", MinimumLength = 2)]
        public string? LastName { get; set; }

        [NotMapped]
        public string FullName 
        { 
            get { return $"{FirstName} {LastName}"; } 
        }

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        public DateTimeOffset? BirthDate
        {
            get => _birthDate;
            set => _birthDate = value?.ToUniversalTime();
        }

        [Required]
        [Display(Name = "Address 1")]
        public string? Address1 { get; set; }

        [Display(Name = "Address 2")]
        public string? Address2 { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        [Display(Name = "Postcode")]
        public string? PostCode { get; set; }
    }
}