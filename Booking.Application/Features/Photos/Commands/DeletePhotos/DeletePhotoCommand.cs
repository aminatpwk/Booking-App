using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Photos.Commands.DeletePhotos
{
    public class DeletePhotoCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
