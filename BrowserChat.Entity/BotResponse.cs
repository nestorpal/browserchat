using BrowserChat.Value;

namespace BrowserChat.Entity
{
    public class BotResponse
    {
        public string Message { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
        public bool CommandError { get; set; }
        public BotCommandErrorType CommandErrorType { get; set; }
    }
}
