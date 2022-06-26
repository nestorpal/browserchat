using Microsoft.AspNetCore.SignalR;

namespace BrowserChat.Backend.Core.HubConfig
{
    public class HubBase : Hub
    {
        public async Task AddToRoom(string roomId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, roomId);
        }

        public async Task RemoveFromRoom(string roomId)
        {
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, roomId);
        }
    }
}
