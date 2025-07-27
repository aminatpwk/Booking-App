using Booking.Application.Repositories;
using Booking.Domain.Users;
namespace Booking.Application.Features.Users
{
    public interface IUserRepository : IGenericRepository<User>
    {
        Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken);
        Task<User> GetUserByEmail(string email, CancellationToken cancellationToken);
    }
}
