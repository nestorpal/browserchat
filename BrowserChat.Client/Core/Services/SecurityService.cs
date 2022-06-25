using BrowserChat.Client.Core.Session;
using BrowserChat.Entity.DTO;

namespace BrowserChat.Client.Core.Services
{
    public class SecurityService : ISecurityService
    {
        private readonly HttpClientHelper _clientHelper;

        public SecurityService(IConfiguration config, SessionManagement sessionMgr)
        {
            _clientHelper = new HttpClientHelper(config["SecurityService"], sessionMgr);
        }


        public UserReadDTO Login(UserLoginDTO login)
        {
            return _clientHelper.PostResponse<UserLoginDTO, UserReadDTO>("user/login", login);
        }

        public UserReadDTO ValidateSession()
        {
            return _clientHelper.GetResponse<object, UserReadDTO>("user", new { });
        }
    }
}
