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
            // Seed a demo user
            await SeedDemoUserAsync(userManager, config);
        }

        public static async Task SeedDemoUserAsync(UserManager<ApplicationUser> userManager, IConfiguration config)
        {
            try
            {
                var demoEmail = config["DemoUserLogin"];
                var demoPassword = config["DemoUserPassword"];

                if (string.IsNullOrEmpty(demoEmail) || string.IsNullOrEmpty(demoPassword))
                {
                    throw new Exception("Demo user login or password not found in configuration, skipping demo user seeding.");
                }

                ApplicationUser demoUser = new()
                {
                    UserName = demoEmail,
                    Email = demoEmail,
                    FirstName = "Demo",
                    LastName = "User",
                    EmailConfirmed = true
                };

                var user = await userManager.FindByEmailAsync(demoUser.Email);

                if (user == null)
                {
                    var result = await userManager.CreateAsync(demoUser, demoPassword);

                    if (!result.Succeeded)
                    {
                        throw new Exception("Failed to create demo user.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine("******* ERROR SEEDING DEMO USER *********");
                Console.WriteLine(ex.Message);
                Console.WriteLine("*****************************************");

                throw;
            }
        }
    }
}