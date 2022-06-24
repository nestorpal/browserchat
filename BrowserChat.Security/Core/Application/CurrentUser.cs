using AutoMapper;
using BrowserChat.Entity.DTO;
using BrowserChat.Security.Core.Entities;
using BrowserChat.Security.Core.JWTLogic;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BrowserChat.Security.Core.Application
{
    public class CurrentUser
    {
        public class CurrentUserCommand : IRequest<UserReadDTO> { }

        public class CurrentUserHandler : IRequestHandler<CurrentUserCommand, UserReadDTO>
        {
            private readonly UserManager<User> _usrManager;
            private readonly IJWTGenerator _jwtGenerator;
            private readonly IMapper _mapper;
            private readonly IHttpContextAccessor _httpAcc;

            public CurrentUserHandler(
                UserManager<User> usrManager,
                IJWTGenerator jwtGenerator,
                IMapper mapper,
                IHttpContextAccessor httpAcc)
            {
                _usrManager = usrManager;
                _jwtGenerator = jwtGenerator;
                _mapper = mapper;
                _httpAcc = httpAcc;
            }

            public async Task<UserReadDTO> Handle(CurrentUserCommand request, CancellationToken cancellationToken)
            {
                var usr = await _usrManager.FindByNameAsync(GetUserSession());

                if (usr != null)
                {
                    var usrDTO = this._mapper.Map<User, UserReadDTO>(usr);

                    usrDTO.Token = this._jwtGenerator.CreateToken(usr);

                    return usrDTO;
                }

                throw new Exception("User not found");
            }

            public string GetUserSession()
            {
                var userName = _httpAcc.HttpContext.User?.Claims?.FirstOrDefault(x => x.Type == "username")?.Value;
                return userName;
            }
        }
    }
}
