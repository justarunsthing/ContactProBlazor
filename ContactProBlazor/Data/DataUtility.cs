using Bogus;
using ContactProBlazor.Models;
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

        public static async Task SeedDemoContactsAsync(UserManager<ApplicationUser> userManager, ApplicationDbContext dbContext, IConfiguration config)
        {
            var demoEmail = config["DemoUserLogin"];

            if (string.IsNullOrEmpty(demoEmail))
            {
                return;
            }

            var demoUser = await userManager.FindByEmailAsync(demoEmail);

            if (demoUser == null)
            {
                return;
            }

            var demoContacts = await dbContext.Contacts
                                              .Where(c => c.AppUserId == demoUser.Id)
                                              .Include(c => c.Categories)
                                              .ToListAsync();

            var demoCategories = await dbContext.Categories
                                                .Where(c => c.AppUserId == demoUser.Id)
                                                .ToListAsync();
            var rnd = new Random();

            if (demoContacts.Count == 0)
            {
                var newContacts = new Faker<Contact>()
                    .RuleFor(c => c.LastName, f => f.Name.LastName())
                    .RuleFor(c => c.BirthDate, f => f.Date.Between(
                                DateTime.Now - TimeSpan.FromDays(365 * 60),
                                DateTime.Now - TimeSpan.FromDays(365 * 18)))
                    .RuleFor(c => c.PhoneNumber, f => f.Phone.PhoneNumber())
                    .RuleFor(c => c.Address1, f => f.Address.StreetAddress())
                    .RuleFor(c => c.City, f => f.Address.City())
                    .RuleFor(c => c.PostCode, f => f.Address.ZipCode("### ###"))
                    .RuleFor(c => c.AppUserId, demoUser.Id)
                    .Generate(10);

                Faker faker = new();
                var imageDir = Path.Combine(Directory.GetCurrentDirectory(), "Data/DemoImages/");
                var menPics = Directory.GetFiles(Path.Combine(imageDir, "Men/")).ToList();
                var womenPics = Directory.GetFiles(Path.Combine(imageDir, "Women/")).ToList();

                for (int i = 0; i < newContacts.Count; i++)
                {
                    Contact contact = newContacts[i];
                    
                    if (i % 2 == 0)
                    {
                        contact.FirstName = faker.Name.FirstName(Bogus.DataSets.Name.Gender.Male);

                        if (menPics.Count > 0)
                        {
                            var pic = menPics[rnd.Next(0, menPics.Count)];
                            menPics.Remove(pic);

                            ImageUpload image = new()
                            {
                                Data = await File.ReadAllBytesAsync(pic),
                                Type = $"image/{Path.GetExtension(pic).TrimStart('.')}"
                            };

                            contact.Image = image;
                            dbContext.Images.Add(image);
                        }
                    }
                    else
                    {
                        contact.FirstName = faker.Name.FirstName(Bogus.DataSets.Name.Gender.Female);

                        if (womenPics.Count > 0)
                        {
                            var pic = womenPics[rnd.Next(0, womenPics.Count)];
                            womenPics.Remove(pic);

                            ImageUpload image = new()
                            {
                                Data = await File.ReadAllBytesAsync(pic),
                                Type = $"image/{Path.GetExtension(pic).TrimStart('.')}"
                            };

                            contact.Image = image;
                            dbContext.Images.Add(image);
                        }
                    }

                    contact.Email = faker.Internet.Email(contact.FirstName, contact.LastName, "mailinator.com");

                    if (rnd.Next() % 2 == 0)
                    {
                        contact.Address2 = new Faker().Address.SecondaryAddress();
                    }
                }
            }
        }
    }
}