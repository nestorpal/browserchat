using BrowserChat.Entity;

namespace BrowserChat.Backend.Core.Data
{
    public class BrowserChatRepository : IBrowserChatRepository
    {
        private readonly BrowserChatDbContext _context;

        public BrowserChatRepository(BrowserChatDbContext context)
        {
            _context = context;
        }

        public IEnumerable<Room> GetAllRooms()
        {
            return _context.Rooms.ToList();
        }

        public IEnumerable<Post> GetRecentPosts(int roomId, int limit)
        {
            return _context.Posts.Where(p => p.RoomId == roomId).OrderByDescending(p => p.TimeStamp).Take(limit).ToList();
        }

        public void RegisterPost(Post post)
        {
            _context.Posts.Add(post);
        }

        public bool SaveChanges()
        {
            return (_context.SaveChanges() >= 0);
        }
    }
}
