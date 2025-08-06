using Booking.Application.Features.Apartments;
using Booking.Infrastructure.GenericRepoImpl;
using Booking.Domain.Apartments;
using Microsoft.EntityFrameworkCore;
using Booking.Domain.Owners;

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
            var query = _context.Apartments.AsQueryable();
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

    }
}

