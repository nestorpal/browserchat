﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Entity
{
    public class Post
    {
        public int Id { get; set; }
        public string UserName { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public DateTime TimeStamp { get; set; }
        public int RoomId { get; set; }
        public Room Room { get; set; }
    }
}
