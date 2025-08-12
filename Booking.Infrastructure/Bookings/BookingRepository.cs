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
                .Include(b => b.Apartment)
                    .ThenInclude(a => a.Photos)
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

        public async Task<bool> IsApartmentAvailable(Guid apartmentId, DateTime start, DateTime end)
        {
            var overlappingBooking = await _context.Bookings.Where(b => b.ApartmentId == apartmentId && (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.PendingApproval)).Where(b => b.Start < end && b.End > start).AnyAsync();
            return !overlappingBooking;
        }
    }
}
