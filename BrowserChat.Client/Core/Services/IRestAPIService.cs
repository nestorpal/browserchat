using BrowserChat.Entity.DTO;

namespace BrowserChat.Client.Core.Services
{
    public interface IRestAPIService
    {
        IEnumerable<RoomReadDTO> GetAllRooms();
        IEnumerable<PostReadDTO> GetRecentPosts(string roomId);
        void PublishPost(PostPublishDTO post);
    }
}
