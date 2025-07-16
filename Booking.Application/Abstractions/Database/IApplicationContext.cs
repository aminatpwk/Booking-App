using Booking.Domain.Apartments;
using Booking.Domain.Users;
using Booking.Domain.Owners;
using Booking.Domain.Bookings;
using Booking.Domain.Reviews;

namespace Booking.Application.Abstractions.Database
{
    public interface IApplicationContext
    {
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}
