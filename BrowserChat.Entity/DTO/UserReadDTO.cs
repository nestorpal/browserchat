using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BrowserChat.Entity.DTO
{
    public class UserReadDTO
    {
        public string UserName { get; set; } = string.Empty;
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
        public string Token { get; set; } = string.Empty;
    }
}
