using MediatR;
using FluentValidation;
using Booking.Domain.Photos;
using Booking.Application.Features.Photos;
using AutoMapper;

namespace Booking.Application.Features.Apartments.Commands.CreateApartment
{
    public class CreateApartmentHandler : IRequestHandler<CreateApartmentCommand, Guid>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IPhotosRepository _photosRepository;
        private readonly IMapper _mapper;
        public CreateApartmentHandler(IApartmentRepository apartmentRepository, IPhotosRepository photosRepository, IMapper mapper)
        {
            _apartmentRepository = apartmentRepository;
            _photosRepository = photosRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateApartmentCommand request, CancellationToken cancellationToken)
        { 
            var owner = await _apartmentRepository.GetOwnerById(request.ApartmentDto.OwnerId, cancellationToken);
            if (owner is null)
            {
                throw new Exception("Owner with this ID does not exist!");
            }

            var isUniqueApartment = await _apartmentRepository.IsApartmentNameUnique(request.ApartmentDto.Name, cancellationToken);
            if (!isUniqueApartment)
            {
                throw new Exception("Apartment with this name already exists!");
            }
            
            var apartment = _mapper.Map<Apartment>(request.ApartmentDto);
            apartment.OwnerId = owner.Id;
            apartment.Owner = owner;
            await _apartmentRepository.Add(apartment);

            var apartmentPhotos = request.ApartmentDto.ImagesBase64;
            if(apartmentPhotos.Count() < 4)
            {
                throw new ValidationException("At least 4 images are required.");
            }

            await AddPhotosToApartmentAsync(apartment, request.ApartmentDto.ImagesBase64, cancellationToken);

            await _apartmentRepository.SaveChangesAsync(cancellationToken);
            return apartment.Id;
        }

        private async Task AddPhotosToApartmentAsync(Apartment apartment, IReadOnlyList<string> imagesBase64, CancellationToken cancellationToken)
        {
            if (imagesBase64.Count() < 4)
            {
                throw new ValidationException("At least 4 images are required.");
            }
            var photos = imagesBase64.Select(base64Image =>
            {
                var base64Data = base64Image.Split(',').Last();
                var imageBytes = Convert.FromBase64String(base64Data);
                return new Photo
                {
                    Id = Guid.NewGuid(),
                    ApartmentId = apartment.Id,
                    ImageData = imageBytes,
                    ImageName = $"{apartment.Id}_{Guid.NewGuid()}.jpg",
                    ImageType = "image/jpeg",
                    CreatedAt = DateTime.UtcNow
                };
            }).ToList();
            await _photosRepository.AddRangeAsync(photos, cancellationToken);
        }
    }
}
