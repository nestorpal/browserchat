using BrowserChat.Entity;
using BrowserChat.Value;
using Microsoft.EntityFrameworkCore;

namespace BrowserChat.Backend.Core.Data
{
    public static class Persistence
    {
        public static void PrepPopulation(IApplicationBuilder builder, bool isProduction)
        {
            using (var serviceScope = builder.ApplicationServices.CreateScope())
            {
                SeedData(serviceScope.ServiceProvider.GetService<BrowserChatDbContext>(), isProduction);
            }
        }

        private static void SeedData(BrowserChatDbContext? context, bool isProduction)
        {
            if (context == null) return;

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

            if (!context.Rooms.Any())
            {
                Constant.Samples.Rooms
                    .ForEach(room => {
                        context.Rooms.Add(new Room { Name = room });
                    });

                context.SaveChanges();
            }

            if (!isProduction
                && !context.Posts.Any())
            {
                Random rnd = new Random();

                for (int i = 1; i <= 100; i++)
                {
                    context.Posts.Add(
                        new Post
                        {
                            Message = $"{Constant.Samples.MessageTemplate}{i}",
                            RoomId = rnd.Next(1, 5),
                            TimeStamp = DateTime.Now,
                            UserName = Constant.Samples.BaseUser.UserName
                        });
                }

                context.SaveChanges();
            }
        }
    }
}
