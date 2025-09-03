using Booking.Application.Features.Apartments;
using Booking.Application.Features.Owners;
using Booking.Application.Features.Users;
using MediatR;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Photos.Commands.DeletePhotos
{
    public class DeletePhotoHandler : IRequestHandler<DeletePhotoCommand, Guid>
    {
        private readonly IPhotosRepository _photosRepository;
        private readonly IApartmentRepository _apartmentRepository;
        private readonly ICurrentUserService _currentUserService;
        private readonly IOwnerRepository _ownerRepository;

        public DeletePhotoHandler(IPhotosRepository photosRepository, IApartmentRepository apartmentRepository, ICurrentUserService currentUserService, IOwnerRepository ownerRepository)
        {
            _photosRepository = photosRepository;
            _apartmentRepository = apartmentRepository;
            _currentUserService = currentUserService;
            _ownerRepository = ownerRepository;
        }   

        public async Task<Guid> Handle(DeletePhotoCommand request, CancellationToken cancellationToken)
        {
            var photo = await _photosRepository.GetById(request.Id);
            if (photo is null)
            {
                throw new Exception("Photo not found!");
            }

            var apartment = await _apartmentRepository.GetById(photo.ApartmentId);
            if (apartment is null)
            {
                throw new Exception("Apartment not found!");
            }

            var currentUserId = _currentUserService.UserId;

            var owner = await _ownerRepository.GetByUserId(currentUserId);
            if(owner is null)
            {
                throw new Exception("You are not registered as an owner.");
            }

            if (apartment.OwnerId != owner.Id)
            {
                throw new UnauthorizedAccessException("You are not authorized to delete this photo.");
            }

            await _photosRepository.Delete(photo);
            return photo.Id;
        }
    }
}
