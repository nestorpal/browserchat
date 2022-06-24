using BrowserChat.Value;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Entity
{
    public class BotResponse
    {
        public string Message { get; set; }
        public string RoomId { get; set; }
        public bool CommandError { get; set; }
        public BotCommandErrorType CommandErrorType { get; set; }
    }
}
