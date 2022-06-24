using AutoMapper;
using BrowserChat.Backend.Core.Data;
using BrowserChat.Backend.Core.HubConfig;
using BrowserChat.Backend.Core.Util;
using BrowserChat.Entity;
using BrowserChat.Entity.DTO;
using Microsoft.AspNetCore.Mvc;

namespace BrowserChat.Backend.Controllers
{
    [Route("/api/chat/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IBrowserChatRepository _repo;
        private readonly IMapper _mapper;
        private readonly HubHelper _hubHelper;

        public PostsController(IBrowserChatRepository repo, IMapper mapper, HubHelper hubHelper)
        {
            _repo = repo;
            _mapper = mapper;
            _hubHelper = hubHelper;
        }

        [HttpGet]
        public ActionResult<IEnumerable<PostReadDTO>> GetRecentPosts(string roomId)
        {
            int roomIdInt = 0;
            if (int.TryParse(General.DecryptStringEncoded(roomId), out roomIdInt))
            {
                return Ok(_mapper.Map<IEnumerable<PostReadDTO>>(_repo.GetRecentPosts(roomIdInt, 50)));
            }

            throw new ArgumentException();
        }

        [HttpPost]
        public async Task PublishPost(PostPublishDTO postPublish)
        {
            if (postPublish != null)
            {
                string userName = "nestor.panu"; // temporarily hardcoded. Afterwards the value will come from HttpContext

                var post = _mapper.Map<Post>(postPublish);
                post.UserName = userName;

                _repo.RegisterPost(post);
                if (_repo.SaveChanges())
                {
                    await _hubHelper.PublishPost(userName, postPublish);
                    return;
                }
            }

            throw new ArgumentNullException();
        }
    }
}
