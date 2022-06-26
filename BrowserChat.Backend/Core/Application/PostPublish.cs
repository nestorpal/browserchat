using AutoMapper;
using BrowserChat.Backend.Core.AsyncServices;
using BrowserChat.Backend.Core.Data;
using BrowserChat.Backend.Core.HubConfig;
using BrowserChat.Entity;
using BrowserChat.Entity.DTO;
using MediatR;
using System.Text.RegularExpressions;

namespace BrowserChat.Backend.Core.Application
{
    public class PostPublish
    {
        public class PostPublishRequest : IRequest<OkResult>
        {
            public string RoomId { get; set; }
            public string Message { get; set; }
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
                    if (IsBotCommand(request.Message, out string command, out string value))
                    {
                        await ProcessBotCommand(command, value, request.RoomId);
                    }
                    else
                    {
                        string userName = GetUserSession();

                        var post = _mapper.Map<Post>(request);
                        post.UserName = userName;

                        _repo.RegisterPost(post);
                        if (_repo.SaveChanges())
                        {
                            await _hubHelper.PublishPost(userName, _mapper.Map<PostPublishDTO>(post));
                        }
                    }

                    return new OkResult();
                }

                throw new ArgumentNullException();
            }

            public string GetUserSession()
            {
                var userName = _httpAccesor.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "username")?.Value;
                return userName;
            }

            private bool IsBotCommand(string message, out string command, out string value)
            {
                bool result = false;
                command = String.Empty;
                value = String.Empty;

                Regex regRule = new Regex("^/([a-zA-Z]+)=(.*)");
                var match = regRule.Match(message);
                if (match.Success)
                {
                    command = match.Groups[1].Value;
                    value = match.Groups[2].Value;
                    result = true;
                }

                return result;
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
