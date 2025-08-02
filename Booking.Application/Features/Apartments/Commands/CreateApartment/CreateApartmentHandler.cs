using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Apartments;
using Booking.Domain.Owners;
using FluentValidation;
namespace Booking.Application.Features.Apartments.Commands.CreateApartment
{
    public class CreateApartmentHandler : IRequestHandler<CreateApartmentCommand, Guid>
    {
        private readonly IApartmentRepository _apartmentRepository;
        private readonly CreateApartmentValidations _validations;
        public CreateApartmentHandler(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
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
            return apartment.Id;
        }
    }
}
