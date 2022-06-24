using BrowserChat.Security.Core.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace BrowserChat.Security.Core.Data
{
    public static class Persistence
    {
        public static async Task PrepPopulation(IApplicationBuilder builder, bool isProduction)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                Migrate(serviceScope.ServiceProvider.GetService<SecurityContext>(), isProduction);
                await SeedData(serviceScope.ServiceProvider.GetService<UserManager<User>>());
            }
        }

        private static void Migrate(SecurityContext context, bool isProduction)
        {
            if (isProduction)
            {
                Console.WriteLine("--> Attempting to apply migrations...");
                try
                {
                    context.Database.Migrate();
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"--> Could not run migrations: {ex.Message}");
                }
            }
        }

        private async static Task SeedData(UserManager<User>? usrManager)
        {

            if (!usrManager.Users.Any())
            {
                var user = new User
                {
                    Name = "Nestor",
                    Surname = "Palacios",
                    UserName = "nestor.panu",
                    Email = "nestor.panu@gmail.com"
                };

                await usrManager.CreateAsync(user, "Password!123");
            }
        }
    }
}
