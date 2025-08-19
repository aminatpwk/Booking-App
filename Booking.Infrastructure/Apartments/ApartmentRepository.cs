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
            DateTime? startDate = null,
            DateTime? endDate = null,
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

            if(startDate.HasValue && endDate.HasValue)
            {
                if(startDate.Value >= endDate.Value)
                {
                    throw new ArgumentException("Start date must be before end date!");
                }

                var start = startDate.Value;
                var end = endDate.Value;
                var bufferDays = 90;
                var thresholdDate = start.AddDays(-bufferDays);

                var availableApartmentsIds = _context.Apartments.Where(a => a.LastBookedOnUtc == null || a.LastBookedOnUtc < thresholdDate).Select(a => a.Id).AsQueryable();
                var bookedApartmentIds = _context.Bookings.Where(b =>
                                                                   (b.Status == BookingStatus.Confirmed || b.Status == BookingStatus.PendingApproval) &&
                                                                   b.End > start.AddDays(-30) && b.Start < end.AddDays(30) && b.Start < end & b.End > start).Select(b => b.ApartmentId).Distinct().AsQueryable();
                var finalIds = availableApartmentsIds.Except(bookedApartmentIds);
                query = query.Where(a => finalIds.Contains(a.Id));
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
    }
}

