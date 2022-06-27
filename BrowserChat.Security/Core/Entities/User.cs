using Microsoft.AspNetCore.Identity;

namespace BrowserChat.Security.Core.Entities
{
    public class User : IdentityUser
    {
        public string Name { get; set; } = string.Empty;
        public string Surname { get; set; } = string.Empty;
    }
}
