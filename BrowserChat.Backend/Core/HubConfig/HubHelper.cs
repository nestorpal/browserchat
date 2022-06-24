using BrowserChat.Entity.DTO;
using Microsoft.AspNetCore.SignalR;

namespace BrowserChat.Backend.Core.HubConfig
{
    public class HubHelper
    {
        private readonly IHubContext<HubBase> _hub;

        public HubHelper(IHubContext<HubBase> hub)
        {
            _hub = hub;
        }

        public async Task PublishPost(string userName, PostPublishDTO post)
        {
            //await _hub.Clients.Group(post.RoomId).SendAsync("ReceiveMessage", post.RoomId, userName, post.Message);
            await _hub.Clients.All.SendAsync("ReceiveMessage", post.RoomId, userName, post.Message);
        }
    }
}
