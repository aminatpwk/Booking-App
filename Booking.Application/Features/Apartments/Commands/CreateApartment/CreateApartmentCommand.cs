using MediatR;
using Booking.Application.Common.DTOs;

namespace Booking.Application.Features.Apartments.Commands.CreateApartment
{
    public class CreateApartmentCommand : IRequest<Guid>
    {
        public ApartmentDto ApartmentDto { get; set; }
    }
}
