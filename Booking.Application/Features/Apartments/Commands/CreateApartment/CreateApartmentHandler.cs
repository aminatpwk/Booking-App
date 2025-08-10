using MediatR;
using FluentValidation;
using Booking.Domain.Photos;
using Booking.Application.Features.Photos;

namespace Booking.Application.Features.Apartments.Commands.CreateApartment
{
    public class CreateApartmentHandler : IRequestHandler<CreateApartmentCommand, Guid>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly IPhotosRepository _photosRepository;
        private readonly CreateApartmentValidations _validations;
        public CreateApartmentHandler(IApartmentRepository apartmentRepository, IPhotosRepository photosRepository)
        {
            _apartmentRepository = apartmentRepository;
            _photosRepository = photosRepository;
            _validations = new CreateApartmentValidations();
        }

        public async Task<Guid> Handle(CreateApartmentCommand request, CancellationToken cancellationToken)
        {
            var isValidResult = await _validations.ValidateAsync(request, cancellationToken);
            if (!isValidResult.IsValid)
            {
                var errors = string.Join(", ", isValidResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }

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
            
            var apartment = Apartment.Create(request.ApartmentDto, owner);
            await _apartmentRepository.Add(apartment);

            if(request.ApartmentDto.ImagesBase64 != null && request.ApartmentDto.ImagesBase64.Any())
            {
                var photos = new List<Photo>();
                foreach (var base64Image in request.ApartmentDto.ImagesBase64)
                {
                    try
                    {
                        var base64Data = base64Image.Split(',').Last();
                        var imageBytes = Convert.FromBase64String(base64Data);
                        var photo = new Photo
                        {
                            Id = Guid.NewGuid(),
                            ApartmentId = apartment.Id,
                            ImageData = imageBytes,
                            ImageName = $"{apartment.Id}_{Guid.NewGuid()}.jpg",
                            ImageType = "image/jpeg",
                            CreatedAt = DateTime.UtcNow
                        };
                        photos.Add(photo);
                    }
                    catch (FormatException ex)
                    {
                        throw new ValidationException("Invalid image format!");
                    }
                }
                await _photosRepository.AddRangeAsync(photos, cancellationToken);
            }

            await _apartmentRepository.SaveChangesAsync(cancellationToken);
            return apartment.Id;
        }
    }
}
