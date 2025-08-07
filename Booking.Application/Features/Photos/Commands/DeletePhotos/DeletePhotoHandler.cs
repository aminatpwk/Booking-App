using Booking.Application.Features.Apartments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Photos.Commands.DeletePhotos
{
    public class DeletePhotoHandler : IRequestHandler<DeletePhotoCommand, Guid>
    {
        private readonly IPhotosRepository _photosRepository;

        public DeletePhotoHandler(IPhotosRepository photosRepository)
        {
            _photosRepository = photosRepository;
        }   

        public async Task<Guid> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            //TO DO: implement logic here
            await _photosRepository.Delete(request.PhotoDto.PhotoId);
            return request.PhotoDto.PhotoId;
        }
    }
}
