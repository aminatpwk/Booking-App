using Booking.Application.Features.Users;
using Booking.Domain.Users;
using Microsoft.EntityFrameworkCore;
using Booking.Infrastructure.GenericRepoImpl;

namespace Booking.Infrastructure.Users
{
    public class UserRepository : GenericRepository<User> , IUserRepository
    {
        private readonly BookingContext _context;
        public UserRepository(BookingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsEmailUnique(string email, CancellationToken cancellationToken)
        {
            var isUnique = await _context.Users.Where(u => u.Email == email).ToListAsync(cancellationToken);
            return !isUnique.Any();
        }

        public async Task<User> GetUserByEmail(string email, CancellationToken cancellationToken)
        {
            var user = await _context.Users.FirstOrDefaultAsync(u => u.Email == email, cancellationToken);
            return user;
        }
    }
}
