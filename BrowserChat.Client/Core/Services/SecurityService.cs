using BrowserChat.Client.Core.Session;
using BrowserChat.Client.Core.Util;
using BrowserChat.Entity.DTO;

namespace BrowserChat.Client.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly HttpClientHelper _clientHelper;

        public SecurityService(SessionManagement sessionMgr)
        {
            _clientHelper = new HttpClientHelper(ConfigurationHelper.SecurityService, sessionMgr);
        }


        public UserReadDTO Login(UserLoginDTO login)
        {
            return _clientHelper.PostResponse<UserLoginDTO, UserReadDTO>("user/login", login);
        }

        public UserReadDTO ValidateSession()
        {
            return _clientHelper.GetResponse<object, UserReadDTO>("user", new { });
        }

        public UserReadDTO Register(UserRegisterDTO register)
        {
            return _clientHelper.PostResponse<UserRegisterDTO, UserReadDTO>("user/register", register);
        }
    }
}
