using BrowserChat.Entity.DTO;

namespace BrowserChat.Client.Core.Services
{
    public interface IRestAPIService
    {
        IEnumerable<RoomReadDTO> GetAllRooms();
    }
}
