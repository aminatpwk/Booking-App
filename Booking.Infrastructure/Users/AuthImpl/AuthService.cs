using Booking.Application.Features.Users.Auth;
using Microsoft.Extensions.Configuration;
using Booking.Domain.Users;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;

namespace Booking.Infrastructure.Users.AuthImpl
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        public AuthService(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        public async Task<string> GenerateToken(User user)
        {
            string secretKey = _configuration.GetSection("JwtConfig").GetSection("SecretKey").Value;
            var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey));
            var expirationTime = _configuration.GetSection("JwtConfig").GetSection("lifetime").Value;
            var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(
                [
                   new Claim(JwtRegisteredClaimNames.Sub, user.Id.ToString()),
                   new Claim(JwtRegisteredClaimNames.Email, user.Email),
                ]),
                Expires = DateTime.UtcNow.AddSeconds(int.Parse(expirationTime)),
                SigningCredentials = credentials,
            };
            var handler = new JsonWebTokenHandler();
            string token = handler.CreateToken(tokenDescriptor);
            return await Task.FromResult(token);
        }

    }
}
