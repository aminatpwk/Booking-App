using Booking.Domain.Users;

namespace Booking.Application.Features.Users.Auth
{
    public interface IAuthService
    {
        Task<string> GenerateToken(User user, string role);
    }
}
