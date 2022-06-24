using Microsoft.EntityFrameworkCore;
using BrowserChat.Entity;

namespace BrowserChat.Backend.Core.Data
{
    public class BrowserChatDbContext : DbContext
    {
        public BrowserChatDbContext(DbContextOptions<BrowserChatDbContext> opt) : base(opt)
        {
            
        }

        public DbSet<Room> Rooms { get; set; }
    }
}
