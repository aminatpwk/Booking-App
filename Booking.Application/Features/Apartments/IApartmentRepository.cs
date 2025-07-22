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
    }
}
