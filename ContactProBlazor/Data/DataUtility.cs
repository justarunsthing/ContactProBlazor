using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ContactProBlazor.Data
{
    public class DataUtility
    {
        // IServiceProvider provides a hook to any services registered
        public static async Task ManageDataAsync(IServiceProvider serviceProvider)
        {
            var dbContextService = serviceProvider.GetRequiredService<ApplicationDbContext>();
            var userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            var config = serviceProvider.GetRequiredService<IConfiguration>();

            // Apply any pending migrations
            await dbContextService.Database.MigrateAsync();
        }
    }
}