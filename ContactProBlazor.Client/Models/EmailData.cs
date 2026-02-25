using System.ComponentModel.DataAnnotations;

namespace ContactProBlazor.Client.Models
{
    public class EmailData
    {
        public required string Recipients { get; set; }

        [Length(5, 100, ErrorMessage = "The email subject must be between 5 and 100 characters")]
        public required string Subject { get; set; }

        [Length(10, 1000, ErrorMessage = "The email body must be between 10 and 1000 characters")]
        public required string Body { get; set; }
    }
}