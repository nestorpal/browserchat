using AutoMapper;
using BrowserChat.Backend.Core.Application;
using BrowserChat.Backend.Core.Data;
using BrowserChat.Backend.Core.Util;
using BrowserChat.Entity.DTO;
using BrowserChat.Value;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

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

        [Authorize]
        [HttpGet]
        public ActionResult<IEnumerable<PostReadDTO>> GetRecentPosts(string roomId)
        {
            int roomIdInt = 0;
            if (int.TryParse(General.DecryptStringEncoded(roomId), out roomIdInt))
            {
                return Ok(_mapper.Map<IEnumerable<PostReadDTO>>(_repo.GetRecentPosts(roomIdInt, Constant.General.MaxPostsPerChatRoom)));
            }

            throw new ArgumentException();
        }

        [Authorize]
        [HttpPost]
        public async Task PublishPost(PostPublish.PostPublishRequest postPublish)
        {
            await _mediator.Send(postPublish);
        }
    }
}
