using AutoMapper;
using BrowserChat.Entity.DTO;
using BrowserChat.Security.Core.Entities;
using BrowserChat.Security.Core.JWTLogic;
using BrowserChat.Value;
using MediatR;
using Microsoft.AspNetCore.Identity;

namespace BrowserChat.Security.Core.Application
{
    public class Login
    {
        public class UsuarioLoginCommand : IRequest<UserReadDTO>
        {
            public string Email { get; set; } = string.Empty;
            public string Password { get; set; } = string.Empty;
        }

        public class UsuarioLoginHandler : IRequestHandler<UsuarioLoginCommand, UserReadDTO>
        {
            private readonly UserManager<User> _usrManager;
            private readonly IMapper _mapper;
            private readonly IJWTGenerator _jwtGenerator;
            private readonly SignInManager<User> _signManager;

            public UsuarioLoginHandler(
                UserManager<User> usrManager,
                IMapper mapper,
                IJWTGenerator jwtGenerator,
                SignInManager<User> signManager)
            {
                _usrManager = usrManager;
                _mapper = mapper;
                _jwtGenerator = jwtGenerator;
                _signManager = signManager;
            }

            public async Task<UserReadDTO> Handle(UsuarioLoginCommand request, CancellationToken cancellationToken)
            {
                var user = await this._usrManager.FindByEmailAsync(request.Email);

                if (user == null)
                    throw new Exception(Constant.MessagesAndExceptions.Security.UserNotFound);

                var result = await _signManager.CheckPasswordSignInAsync(user, request.Password, false);

                if (result.Succeeded)
                {
                    var userDTO = _mapper.Map<User, UserReadDTO>(user);

                    userDTO.Token = this._jwtGenerator.CreateToken(user);

                    return userDTO;
                }

                throw new Exception(Constant.MessagesAndExceptions.Security.IncorrectLogin);
            }
        }
    }
}
