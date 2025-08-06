using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Features.Photos;
using Booking.Infrastructure.GenericRepoImpl;
using Booking.Domain.Photos;

namespace Booking.Infrastructure.Photos
{
    public class PhotosRepository : GenericRepository<Photo>, IPhotosRepository
    {
        private readonly BookingContext _context;
        public PhotosRepository(BookingContext dbContext) : base(dbContext)
        {
            _context = dbContext;
        }

        public async Task AddRangeAsync(IEnumerable<Photo> photos, CancellationToken cancellationToken)
        {
            await _context.Photos.AddRangeAsync(photos, cancellationToken);
        }
    }
}
