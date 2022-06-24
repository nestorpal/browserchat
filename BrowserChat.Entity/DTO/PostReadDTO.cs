using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Entity.DTO
{
    public class PostReadDTO
    {
        public string UserName { get; set; }
        public string Message { get; set; }
        public string TimeStampStr { get; set; }
        public string RoomId { get; set; }
    }
}
