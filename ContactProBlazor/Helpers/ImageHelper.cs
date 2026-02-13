using ContactProBlazor.Models;
using System.Text.RegularExpressions;

namespace ContactProBlazor.Helpers
{
    public static class ImageHelper
    {
        public static readonly string DefaultProfilePictureUrl = "/img/default-profile-picture.jpg";

        public static async Task<ImageUpload> GetImageUploadAsync(IFormFile file)
        {
            using var ms = new MemoryStream();
            await file.CopyToAsync(ms);
            byte[] data = ms.ToArray();

            if (ms.Length > 1 * 1024 * 1024)
            {
                throw new Exception("The image size cannot exceed 1 MB.");
            }

            var imageUpload = new ImageUpload
            {
                Id = Guid.NewGuid(),
                Data = data,
                Type = file.ContentType
            };

            return imageUpload;
        }

        public static ImageUpload GetImageUploadFromUrl(string dataUrl)
        {
            // Get segments out of the data URL
            // Example: data:image/png;base64,iVBORw0KGgoAAAANSUhEUgAAAoAAAAHgCAYAAACp8...
            // Matched strings are shoved into <type> & <data> key-value pairs
            GroupCollection matchGroups  = Regex.Match(dataUrl, @"data:(?<type>.+?);base64,(?<data>.+)").Groups;

            if (matchGroups.ContainsKey("type") && matchGroups.ContainsKey("data"))
            {
                // Image type
                string contentType = matchGroups["type"].Value;
                // Image data
                byte[] data = Convert.FromBase64String(matchGroups["data"].Value);

                if (data.Length <= 5 * 1024 * 1024)
                {
                    ImageUpload upload = new ImageUpload()
                    {
                        Id = Guid.NewGuid(),
                        Data = data,
                        Type = contentType
                    };

                    return upload;
                }
            }

            throw new IOException("Data URL was either invalid or too large!");
        }
    }
}