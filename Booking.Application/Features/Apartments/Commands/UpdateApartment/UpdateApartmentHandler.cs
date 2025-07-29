using Booking.Application.Features.Apartments.Commands.CreateApartment;
using MediatR;
using Booking.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Commands.UpdateApartment
{
    public class UpdateApartmentHandler : IRequestHandler<CreateApartmentCommand, Guid>
    {
        private readonly IApartmentRepository _repository;
        public UpdateApartmentHandler(IApartmentRepository repository)
        {
            _repository = repository;
        }

        public async Task<Guid> Handle(CreateApartmentCommand request, CancellationToken cancellationToken)
        {
            var apartmentOwner = await _repository.GetOwnerById(request.ApartmentDto.OwnerId, cancellationToken);
            if(apartmentOwner is null)
            {
                throw new Exception("Owner of apartment not found!");
            }

            var uniqueApartment = await _repository.IsApartmentNameUnique(request.ApartmentDto.Name, cancellationToken);
            if (!uniqueApartment)
            {
                throw new Exception("Apartment name must be unique!");
            }
            var apartment = Apartment.Create(request.ApartmentDto, apartmentOwner);
            await _repository.Add(apartment);
            return apartment.Id;
        }
    }
}
