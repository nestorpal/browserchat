namespace BrowserChat.Entity
{
    public class BotRequest
    {
        public string Command { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
    }
}
