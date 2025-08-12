using Booking.Application.Repositories;
using Booking.Domain.Bookings;
using System.Linq.Expressions;

namespace Booking.Application.Features.Bookings
{
    public interface IBookingRepository : IGenericRepository<BookingEntity>
    {
        Task<List<BookingEntity>> GetAllBookingsPerUser(Guid userId);
        Task<BookingEntity?> GetLastBooking(Guid userId, Guid apartmentId);
        Task<bool> IsApartmentAvailable(Guid apartmentId, DateTime start, DateTime end);
    }
}
