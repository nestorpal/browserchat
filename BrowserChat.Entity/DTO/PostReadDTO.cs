namespace BrowserChat.Entity.DTO
{
    public class PostReadDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string TimeStampStr { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
    }
}
