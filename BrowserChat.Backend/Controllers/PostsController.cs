using AutoMapper;
using BrowserChat.Backend.Core.Data;
using BrowserChat.Backend.Core.HubConfig;
using BrowserChat.Backend.Core.Util;
using BrowserChat.Entity;
using BrowserChat.Entity.DTO;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using BrowserChat.Backend.Core.Application;

namespace BrowserChat.Backend.Controllers
{
    [Route("/api/chat/[controller]")]
    [ApiController]
    public class PostsController : ControllerBase
    {
        private readonly IBrowserChatRepository _repo;
        private readonly IMapper _mapper;
        private readonly IMediator _mediator;

        public PostsController(
            IBrowserChatRepository repo,
            IMapper mapper,
            IMediator mediator)
        {
            _repo = repo;
            _mapper = mapper;
            _mediator = mediator;
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
        public async Task PublishPost(PostPublish.PostPublishRequest postPublish)
        {
            await _mediator.Send(postPublish);
        }
    }
}
