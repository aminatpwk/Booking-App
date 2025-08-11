using Booking.Application.Features.Bookings;
using Booking.Domain.Bookings;
using Booking.Infrastructure.GenericRepoImpl;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Bookings
{
    public class BookingRepository(BookingContext dbContext) : GenericRepository<BookingEntity>(dbContext), IBookingRepository
    {
        private readonly BookingContext _context = dbContext;

        public async Task<List<BookingEntity>> GetAllBookingsPerUser(Guid userId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId)
                .ToListAsync();
        }

        public async Task<BookingEntity?> GetLastBooking(Guid userId, Guid apartmentId)
        {
            return await _context.Bookings
                .Where(b => b.UserId == userId && b.ApartmentId == apartmentId)
                .OrderByDescending(b => b.End)
                .FirstOrDefaultAsync();
        }
    }
}
