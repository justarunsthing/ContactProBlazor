using Microsoft.AspNetCore.Components.Forms;

namespace ContactProBlazor.Client.Helpers
{
    public class BrowserFileHelper
    {
        public static readonly string DefaultContactImage = "img/default-contact-image.png";
        public static int MaxFileSize = 5 * 1024 * 1024; // 5 MB

        public static async Task<string> GetImageDataUrl(IBrowserFile file)
        {
            using Stream fileStream = file.OpenReadStream(MaxFileSize);
            using MemoryStream memoryStream = new();

            await fileStream.CopyToAsync(memoryStream);

            byte[] imageBytes = memoryStream.ToArray();
            string imageBase64 = Convert.ToBase64String(imageBytes);
            string imageDataUrl = $"data:{file.ContentType};base64,{imageBase64}";

            return imageDataUrl;
        }
    }
}