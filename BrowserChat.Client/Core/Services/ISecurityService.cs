using BrowserChat.Entity.DTO;

namespace BrowserChat.Client.Core.Services
{
    public interface ISecurityService
    {
        UserReadDTO Login(UserLoginDTO login);
        UserReadDTO ValidateSession();
        UserReadDTO Register(UserRegisterDTO register);
    }
}
