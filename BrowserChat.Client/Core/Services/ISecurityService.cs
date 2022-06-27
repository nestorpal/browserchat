using BrowserChat.Entity.DTO;

namespace BrowserChat.Client.Core.Services
{
    public interface ISecurityService
    {
        /// <summary>
        /// Calls an Identity Rest API to validate login data
        /// </summary>
        /// <param name="login"></param>
        /// <returns>A model with the user data which includes a security token</returns>
        UserReadDTO Login(UserLoginDTO login);

        /// <summary>
        /// Calls an Identity Rest API to validate security token
        /// </summary>
        /// <returns></returns>
        UserReadDTO ValidateSession();

        /// <summary>
        /// Calls an Identity Rest API to register a new user
        /// </summary>
        /// <param name="register"></param>
        /// <returns></returns>
        UserReadDTO Register(UserRegisterDTO register);
    }
}
