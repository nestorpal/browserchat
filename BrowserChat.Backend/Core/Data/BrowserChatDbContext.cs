using Microsoft.EntityFrameworkCore;
using BrowserChat.Entity;

namespace BrowserChat.Backend.Core.Data
{
    public class BrowserChatDbContext : DbContext
    {
        public BrowserChatDbContext(DbContextOptions<BrowserChatDbContext> opt) : base(opt)
        {
            
        }

        protected override void OnModelCreating(Microsoft.EntityFrameworkCore.ModelBuilder builder)
        {
            builder.Entity<Room>()
                .HasMany(r => r.Posts)
                .WithOne(p => p.Room)
                .HasForeignKey(p => p.RoomId);

            builder.Entity<Post>()
                .HasAlternateKey(p => new { p.RoomId, p.TimeStamp });

            builder.Entity<Post>()
                .HasOne(p => p.Room)
                .WithMany(r => r.Posts)
                .HasForeignKey(p => p.RoomId);
        }

        public DbSet<Room> Rooms { get; set; }

        public DbSet<Post> Posts { get; set; }
    }
}
