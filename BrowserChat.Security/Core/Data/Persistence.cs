using BrowserChat.Security.Core.Entities;
using BrowserChat.Value;
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

        private static void Migrate(SecurityContext? context, bool isProduction)
        {
            if (context != null)
            {
                if (isProduction)
                {
                    try
                    {
                        context.Database.Migrate();
                    }
                    catch (Exception ex)
                    {
                    }
                }
            }
        }

        private async static Task SeedData(UserManager<User>? usrManager)
        {
            if (usrManager != null)
            {
                if (!usrManager.Users.Any())
                {
                    var user = new User
                    {
                        Name = Constant.Samples.BaseUser.Name,
                        Surname = Constant.Samples.BaseUser.Surname,
                        UserName = Constant.Samples.BaseUser.UserName,
                        Email = Constant.Samples.BaseUser.Email
                    };

                    await usrManager.CreateAsync(user, Constant.Samples.BaseUser.Password);
                }
            }
        }
    }
}
