using BrowserChat.Client.Core.Services;
using BrowserChat.Client.Core.Session;
using BrowserChat.Client.Models;
using BrowserChat.Entity.DTO;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace BrowserChat.Client.Controllers
{
    public class HomeController : Controller
    {
        private readonly IConfiguration _config;
        private readonly ISecurityService _securityService;
        private readonly IRestAPIService _restApiService;
        private readonly SessionManagement _sessionMgr;

        public HomeController(
            IConfiguration config,
            ISecurityService securityService,
            IRestAPIService restApiService,
            SessionManagement sessionMgr)
        {
            _config = config;
            _securityService = securityService;
            _restApiService = restApiService;
            _sessionMgr = sessionMgr;
        }

        [HttpGet]
        public IActionResult Index()
        {
            if (_sessionMgr.IsLoggedIn())
            {
                var user = _securityService.ValidateSession();
                if (string.IsNullOrEmpty(user?.Token))
                {
                    _sessionMgr.RemoveUserSession();
                    return RedirectToAction("Login", "Account");
                }
                else
                {
                    ViewBag.hubService = _config["HubService"];
                    List<RoomReadDTO> roomList = _restApiService.GetAllRooms().ToList();
                    return View(roomList);
                }
            }
            else
            {
                return RedirectToAction("Login", "Account");
            }
        }

        [HttpGet]
        public JsonResult GetRecentPosts(string room)
        {
            var result = _restApiService.GetRecentPosts(room).Reverse().ToList();

            return new JsonResult(result);
        }

        [HttpPost]
        public JsonResult PublishPost(PostPublishDTO post)
        {
            bool result = false;
            try
            {
                _restApiService.PublishPost(post);
                result = true;
            }
            catch { }
            

            return new JsonResult(new { result = result });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}