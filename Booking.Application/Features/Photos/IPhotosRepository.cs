using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Photos;
using Booking.Application.Repositories;
namespace Booking.Application.Features.Photos
{
    public interface IPhotosRepository : IGenericRepository<Photo>
    {
        Task AddRangeAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken);
    }
}
