using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Commands.DeleteApartment
{
    public class DeleteApartmentHandler : IRequestHandler<DeleteApartmentCommand, Guid>
    {
        public readonly IApartmentRepository _apartmentRepository;
        public DeleteApartmentHandler(IApartmentRepository apartmentRepository)
        {
            _apartmentRepository = apartmentRepository;
        }

        public async Task<Guid> Handle(DeleteApartmentCommand request, CancellationToken cancellationToken)
        {
            var apartment = await _apartmentRepository.GetById(request.Id);
            if (apartment is null)
            {
                throw new Exception("Apartment not found!");
            }
             await _apartmentRepository.Delete(apartment);
            return apartment.Id;
        }
    }
}
