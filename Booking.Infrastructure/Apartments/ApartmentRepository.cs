using Booking.Application.Features.Apartments;
using Booking.Infrastructure.GenericRepoImpl;
using Booking.Domain.Apartments;
using Microsoft.EntityFrameworkCore;
using Booking.Domain.Owners;
using Booking.Domain.Bookings;

namespace Booking.Infrastructure.Apartments
{
    public class ApartmentRepository(BookingContext dbContext) : GenericRepository<Apartment>(dbContext), IApartmentRepository
    {
        private readonly BookingContext _context = dbContext;

        public async Task<bool> IsApartmentNameUnique(string name, CancellationToken cancellationToken)
        {
            var isUnique = await _context.Apartments.Where(a => a.Name == name).ToListAsync(cancellationToken);
            return !isUnique.Any();
        }

        public async Task<Owner> GetOwnerById(Guid ownerId, CancellationToken cancellationToken)
        {
            var owner = await _context.Owners.FirstOrDefaultAsync(o => o.Id == ownerId, cancellationToken);
            return owner ?? throw new Exception("Owner with this ID does not exist!");
        }

        public async Task<(List<Apartment> apartments, int totalCount)> GetPagedAsync(
            int pageIndex,
            int pageSize,
            string? searchTerm = null,
            string? sortBy = null,
            bool sortDescending = false,
            string? country = null,
            string? city = null,
            ApartmentType? type = null,
            decimal? minPrice = null,
            decimal? maxPrice = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Apartments
                .Include(a => a.Photos)
                //.Include(a => a.Owner)
                .AsQueryable();
            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(a =>
                a.Name.Contains(searchTerm) ||
                a.Country.Contains(searchTerm) ||
                a.City.Contains(searchTerm));
            }

            if(!string.IsNullOrWhiteSpace(country))
            {
                query = query.Where(a => a.Country.Contains(country));
            }

            if(!string.IsNullOrWhiteSpace(city))
            {
                query = query.Where(a => a.City.Contains(city));
            }

            if(type.HasValue)
            {
                query = query.Where(a => a.Type == type.Value);
            }

            if(minPrice.HasValue)
            {
                query = query.Where(a => a.Price >= minPrice.Value);
            }

            if(maxPrice.HasValue)
            {
                query = query.Where(a => a.Price <= maxPrice.Value);
            }

            if (!string.IsNullOrEmpty(sortBy))
            {
                if (sortBy.Equals("Price", StringComparison.OrdinalIgnoreCase))
                {
                    query = sortDescending ? query.OrderByDescending(a => a.Price) : query.OrderBy(a => a.Price);
                } else if(sortBy.Equals("Rating", StringComparison.OrdinalIgnoreCase))
                {
                    if (sortDescending)
                    {
                        query = query.OrderByDescending(a => a.Reviews.Any() ? a.Reviews.Average(r => r.Rating) : 0);
                    }
                    else
                    {
                        query = query.OrderBy(a => a.Reviews.Any() ? a.Reviews.Average(r => r.Rating) : 0);
                    }
                }
                else
                {
                    query = query.OrderBy(a => a.Id);
                }
            }

            var totalCount = await query.CountAsync(cancellationToken);
            var apartments = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return (apartments, totalCount);
        }

        public async Task<bool> UpdateLastBookedOnUtc(Guid apartmentId, DateTime bookingStartDate, CancellationToken cancellationToken)
        {
            try
            {
                var apartment = await _context.Apartments.FirstOrDefaultAsync(a => a.Id == apartmentId, cancellationToken);
                if(apartment == null)
                {
                    return false;
                }

                if(apartment.LastBookedOnUtc == null || bookingStartDate > apartment.LastBookedOnUtc.Value)
                {
                    apartment.SetLastBookedOnUtc(bookingStartDate);
                    var affectedRows = await _context.SaveChangesAsync(cancellationToken);
                    return affectedRows > 0;
                }

                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<IQueryable<Apartment>> GetAvailableApartments(DateTime startDate, DateTime endDate)
        {
            if (startDate >= endDate)
            {
                throw new ArgumentException("Start date must be before end date!");
            }

            var bufferDays = 90;
            var thresholdDate = startDate.AddDays(-bufferDays);

            //kontrollohet nese apartamenti ka patur booking ne kohet e fundit apo jo, nese jo merret si available, nese po atehere
            //kontrollohen bookings aktual te lidhur me te te cilet ndikohen nga periudha e datave te filtrimit
            return _context.Apartments.Where(apartment => apartment.LastBookedOnUtc == null || apartment.LastBookedOnUtc < thresholdDate ||
                (apartment.LastBookedOnUtc >= thresholdDate && !_context.Bookings.Where(booking => booking.ApartmentId == apartment.Id && (booking.Status == BookingStatus.Confirmed || booking.Status == BookingStatus.PendingApproval) &&
                 booking.End > startDate.AddDays(-30) && booking.Start < endDate.AddDays(30)).Any(booking => booking.Start < endDate && booking.End > startDate)));
        }
    }
}

