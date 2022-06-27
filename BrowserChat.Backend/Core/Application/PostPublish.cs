using AutoMapper;
using BrowserChat.Backend.Core.AsyncServices;
using BrowserChat.Backend.Core.Data;
using BrowserChat.Backend.Core.HubConfig;
using BrowserChat.Entity;
using BrowserChat.Entity.DTO;
using MediatR;

namespace BrowserChat.Backend.Core.Application
{
    public class PostPublish
    {
        public class PostPublishRequest : IRequest<OkResult>
        {
            public string RoomId { get; set; } = string.Empty;
            public string Message { get; set; } = string.Empty;
        }

        public class OkResult
        {

        }

        public class PostPublishHandler : IRequestHandler<PostPublishRequest, OkResult>
        {
            private readonly HubHelper _hubHelper;
            private readonly IMapper _mapper;
            private readonly IBrowserChatRepository _repo;
            private readonly IHttpContextAccessor _httpAccesor;

            public PostPublishHandler(
                HubHelper hubHelper,
                IMapper mapper,
                IBrowserChatRepository repo,
                IHttpContextAccessor httpAccesor)
            {
                _hubHelper = hubHelper;
                _mapper = mapper;
                _repo = repo;
                _httpAccesor = httpAccesor;
            }

            public async Task<OkResult> Handle(PostPublishRequest request, CancellationToken cancellationToken)
            {
                if (request != null)
                {
                    var post = _mapper.Map<Post>(request);

                    if (BrowserChat.Util.Bot.IsBotCommand(post.Message, out string command, out string value))
                    {
                        await ProcessBotCommand(command, value, request.RoomId);
                        return new OkResult();
                    }
                    else
                    {
                        string userName = GetUserSession();

                        if (!string.IsNullOrEmpty(userName))
                        {
                            post.UserName = userName;

                            _repo.RegisterPost(post);
                            if (_repo.SaveChanges())
                            {
                                await _hubHelper.PublishPost(userName, _mapper.Map<PostPublishDTO>(post));
                            }

                            return new OkResult();
                        }
                    }
                }

                throw new ArgumentNullException();
            }

            public string GetUserSession()
            {
                var userName = _httpAccesor.HttpContext?.User?.Claims?.FirstOrDefault(x => x.Type == "username")?.Value;
                return userName ?? string.Empty ;
            }

            private async Task ProcessBotCommand(
                string command,
                string value,
                string roomId)
            {
                await Task.Run(() =>
                {
                    new BotRequestPublisher().Publish(
                        new BotRequest
                        {
                            Command = command,
                            Value = value,
                            RoomId = roomId
                        }
                    );
                });
            }
        }
    }
}
