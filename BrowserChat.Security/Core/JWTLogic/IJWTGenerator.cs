using BrowserChat.Security.Core.Entities;

namespace BrowserChat.Security.Core.JWTLogic
{
    public interface IJWTGenerator
    {
        /// <summary>
        /// Generates a Json Web Token with the user data and a secret key
        /// </summary>
        /// <param name="usr"></param>
        /// <returns>A Json Web Token</returns>
        string CreateToken(User usr);
    }
}
