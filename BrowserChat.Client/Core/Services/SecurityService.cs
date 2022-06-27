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
            UserReadDTO result;
            try
            {
                result = _clientHelper.PostResponse<UserLoginDTO, UserReadDTO>("user/login", login);
            }
            catch
            {
                result = new UserReadDTO { Token = string.Empty };
            }
            return result;
        }

        public UserReadDTO ValidateSession()
        {
            UserReadDTO result;
            try
            {
                result  = _clientHelper.GetResponse<object, UserReadDTO>("user", new { });
            }
            catch
            {
                result = new UserReadDTO { Token = string.Empty };
            }
            return result;
        }

        public UserReadDTO Register(UserRegisterDTO register)
        {
            UserReadDTO result;
            try
            {
                result = _clientHelper.PostResponse<UserRegisterDTO, UserReadDTO>("user/register", register);
            }
            catch
            {
                result = new UserReadDTO { Token = string.Empty };
            }
            return result;
        }
    }
}
