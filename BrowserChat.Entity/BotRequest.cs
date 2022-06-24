using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Entity
{
    public class BotRequest
    {
        public string Command { get; set; }
        public string Value { get; set; }
        public string RoomId { get; set; }
    }
}
