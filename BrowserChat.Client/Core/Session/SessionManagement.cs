using BrowserChat.Entity.DTO;
using System.Text.Json;

namespace BrowserChat.Client.Core.Session
{
    public class SessionManagement
    {
        private readonly IHttpContextAccessor _httpAccesor;

        private readonly string _userSessionKeyword = Keyword.User;
        private readonly string _sessionTokenKeyword = Keyword.SessionToken;
        private readonly string _isLoggedInKeyword = Keyword.IsLoggedIn;
        private readonly string _userFullNameKeyword = Keyword.UserFullName;

        public SessionManagement(IHttpContextAccessor httpAccesor)
        {
            _httpAccesor = httpAccesor;
        }

        public void SetUserSession(UserReadDTO userSession)
        {
            _httpAccesor.HttpContext?.Session.SetString(_userSessionKeyword, JsonSerializer.Serialize(userSession));
            _httpAccesor.HttpContext?.Session.SetString(_sessionTokenKeyword, userSession.Token);
            _httpAccesor.HttpContext?.Session.SetString(_isLoggedInKeyword, "Y");
            _httpAccesor.HttpContext?.Session.SetString(_userFullNameKeyword, $"{userSession.Name} {userSession.Surname}");
        }

        public string GetSessionToken()
        {
            return _httpAccesor.HttpContext.Session.GetString(_sessionTokenKeyword);
        }

        public void RemoveUserSession()
        {
            _httpAccesor.HttpContext?.Session.SetString(_userSessionKeyword, string.Empty);
            _httpAccesor.HttpContext?.Session.SetString(_sessionTokenKeyword, string.Empty);
            _httpAccesor.HttpContext?.Session.SetString(_isLoggedInKeyword, string.Empty);
        }

        public bool IsLoggedIn()
        {
            return _httpAccesor.HttpContext.Session.GetString(_isLoggedInKeyword) == "Y";
        }
    }
}
