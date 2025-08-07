using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Photos.Commands.DeletePhotos
{
    public class PhotoDto
    {
        public Guid PhotoId { get; init; }
        public Guid ApartmentId { get; init; }
    }
}
