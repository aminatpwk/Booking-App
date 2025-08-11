using Booking.Domain.Bookings;
using Booking.Application.Repositories;

namespace Booking.Application.Features.Bookings
{
    public interface IBookingRepository : IGenericRepository<BookingEntity>
    {
        Task<List<BookingEntity>> GetAllBookingsPerUser(Guid userId);
        Task<BookingEntity?> GetLastBooking(Guid userId, Guid apartmentId);
    }
}
