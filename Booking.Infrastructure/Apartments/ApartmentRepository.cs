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
            string? sortBy = null,
            bool sortDescending = false,
            string? searchTerm = null,
            CancellationToken cancellationToken = default)
        {
            var query = _context.Apartments.AsQueryable();
            var totalCount = await query.CountAsync(cancellationToken);
            var apartments = await query.Skip(pageIndex * pageSize).Take(pageSize).ToListAsync(cancellationToken);

            return (apartments, totalCount);
        }

        //TO DO: apply sorting
    }
}

