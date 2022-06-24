using BrowserChat.Security.Core.Entities;

namespace BrowserChat.Security.Core.JWTLogic
{
    public interface IJWTGenerator
    {
        string CreateToken(User usr);
    }
}
