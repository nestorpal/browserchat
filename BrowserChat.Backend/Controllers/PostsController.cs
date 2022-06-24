using AutoMapper;
using BrowserChat.Backend.Core.Data;
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

        public PostsController(IBrowserChatRepository repo, IMapper mapper)
        {
            _repo = repo;
            _mapper = mapper;
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
    }
}
