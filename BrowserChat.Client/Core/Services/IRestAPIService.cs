using BrowserChat.Entity.DTO;

namespace BrowserChat.Client.Core.Services
{
    public interface IRestAPIService
    {
        /// <summary>
        /// Calls a Rest API and returns the Room List
        /// </summary>
        /// <returns></returns>
        IEnumerable<RoomReadDTO> GetAllRooms();

        /// <summary>
        /// Calls a Rest API and returns the most recent Posts of a Room
        /// </summary>
        /// <param name="roomId"></param>
        /// <returns></returns>
        IEnumerable<PostReadDTO> GetRecentPosts(string roomId);

        /// <summary>
        /// Calls a Rest API to publish a new Post to the respective Room
        /// </summary>
        /// <param name="post"></param>
        void PublishPost(PostPublishDTO post);
    }
}
