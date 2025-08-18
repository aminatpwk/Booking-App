using Booking.Application.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Apartments;
using Booking.Domain.Owners;
namespace Booking.Application.Features.Apartments
{
    public interface IApartmentRepository : IGenericRepository<Apartment>
    {
        Task<bool> IsApartmentNameUnique(string name, CancellationToken cancellationToken);
        Task<Owner> GetOwnerById(Guid ownerId, CancellationToken cancellationToken);
        Task<(List<Apartment> apartments, int totalCount)> GetPagedAsync(
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
            CancellationToken cancellationToken = default);
        Task<IQueryable<Apartment>> GetAvailableApartments(DateTime startDate, DateTime endDate);
        Task<bool> UpdateLastBookedOnUtc(Guid apartmentId, DateTime bookingStartDate, CancellationToken cancellationToken);
    }
}
