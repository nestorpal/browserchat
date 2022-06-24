using BrowserChat.Security.Core.Entities;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace BrowserChat.Security.Core.JWTLogic
{
    public class JWTGenerator : IJWTGenerator
    {
        private readonly IConfiguration _config;

        public JWTGenerator(IConfiguration config)
        {
            _config = config;
        }

        public string CreateToken(User usr)
        {
            var claims = new List<Claim>
            {
                new Claim("username", usr.UserName),
                new Claim("name", usr.Name),
                new Claim("surname", usr.Surname)
            };

            var key = new SymmetricSecurityKey(System.Text.Encoding.UTF8.GetBytes(_config.GetValue<string>("JWTKey")));
            var credentials = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credentials
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return tokenHandler.WriteToken(token);
        }
    }
}
