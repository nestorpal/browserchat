using BrowserChat.Entity;

namespace BrowserChat.Backend.Core.Data
{
    public interface IBrowserChatRepository
    {
        /// <summary>
        /// Lists existing Rooms
        /// </summary>
        /// <returns>The Rooms List</returns>
        IEnumerable<Room> GetAllRooms();

        /// <summary>
        /// Lists existing recent Posts of a Room based on a limit number
        /// </summary>
        /// <param name="roomId"></param>
        /// <param name="limit"></param>
        /// <returns>The last N Posts of a Room </returns>
        IEnumerable<Post> GetRecentPosts(int roomId, int limit);
        
        /// <summary>
        /// Inserts a new Post in the database
        /// </summary>
        /// <param name="post"></param>
        void RegisterPost(Post post);
        
        /// <summary>
        /// Makes changes persistent on the database
        /// </summary>
        /// <returns></returns>
        bool SaveChanges();
    }
}
