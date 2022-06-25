using BrowserChat.Client.Core.Session;
using BrowserChat.Entity.DTO;

namespace BrowserChat.Client.Core.Services
{
    public class RestAPIService : IRestAPIService
    {
        private readonly HttpClientHelper _clientHelper;

        public RestAPIService(IConfiguration config, SessionManagement sessionMgr)
        {
            _clientHelper = new HttpClientHelper(config["RestAPIService"], sessionMgr);
        }


        public IEnumerable<RoomReadDTO> GetAllRooms()
        {
            return _clientHelper.GetResponse<object, IEnumerable<RoomReadDTO>>("chat/rooms", new { });
        }

        public IEnumerable<PostReadDTO> GetRecentPosts(string roomId)
        {
            return _clientHelper.GetResponse<object, IEnumerable<PostReadDTO>>("chat/posts", new { roomId = roomId });
        }
    }
}
