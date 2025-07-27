using Booking.Application.Features.Users.Auth;
using Microsoft.Extensions.Configuration;
using Booking.Domain.Users;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using Microsoft.IdentityModel.JsonWebTokens;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Users.AuthImpl
{
    public class AuthService : IAuthService
    {
        private readonly IConfiguration _configuration;
        private readonly BookingContext _context;
        public AuthService(IConfiguration configuration, BookingContext context)
        {
            _configuration = configuration;
            _context = context;
        }

        public async Task<string> GenerateToken(User user)
        {
            user = await _context.Users
                .Include(u => u.Owner)
                .FirstOrDefaultAsync(u => u.Id == user.Id);
            var role = user.Owner is not null ? "Owner" : "User";

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
                   new Claim(ClaimTypes.Role, role),
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
