using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Entity
{
    public class BotRequest
    {
        public string Command { get; set; } = string.Empty;
        public string Value { get; set; } = string.Empty;
        public string RoomId { get; set; } = string.Empty;
    }
}
