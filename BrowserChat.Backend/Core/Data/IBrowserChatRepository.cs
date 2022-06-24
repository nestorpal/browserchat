using BrowserChat.Entity;

namespace BrowserChat.Backend.Core.Data
{
    public interface IBrowserChatRepository
    {
        IEnumerable<Room> GetAllRooms();
        IEnumerable<Post> GetRecentPosts(int roomId, int limit);
    }
}
