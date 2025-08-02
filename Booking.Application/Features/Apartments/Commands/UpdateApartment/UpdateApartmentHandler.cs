using Booking.Application.Features.Apartments.Commands.CreateApartment;
using MediatR;
using Booking.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;

namespace Booking.Application.Features.Apartments.Commands.UpdateApartment
{
    public class UpdateApartmentHandler : IRequestHandler<UpdateApartmentCommand, Guid>
    {
        private readonly IApartmentRepository _repository;
        public UpdateApartmentHandler(IApartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(UpdateApartmentCommand request, CancellationToken cancellationToken)
        {
            var apartment = await _repository.GetById(request.Id);
            if(apartment == null)
            {
                throw new Exception($"Apartment with ID {request.Id} not found!");
            }

            var apartmentOwner = await _repository.GetOwnerById(request.ApartmentDetailDto.OwnerId, cancellationToken);
            if(apartmentOwner == null)
            {
                throw new Exception("Owner of apartment not found!");
            }

            if(apartment.Name != request.ApartmentDetailDto.Name)
            {
                var uniqueApartment = await _repository.IsApartmentNameUnique(request.ApartmentDetailDto.Name, cancellationToken);
                if (!uniqueApartment)
                {
                    throw new Exception("Apartment name must be unique!");
                }
            }

            apartment.UpdateApartment(
                request.ApartmentDetailDto.Name,
                request.ApartmentDetailDto.Address,
                request.ApartmentDetailDto.Price,
                request.ApartmentDetailDto.Description,
                request.ApartmentDetailDto.CleaningFee,
                request.ApartmentDetailDto.Amenities);
            await _repository.Update(apartment);
            return apartment.Id;
        }
    }
}
