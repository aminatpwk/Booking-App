using MediatR;
using Booking.Domain.Apartments;
namespace Booking.Application.Features.Apartments.Commands.CreateApartment
{
    public class CreateApartmentCommand : IRequest<Guid>
    {
        public ApartmentDto ApartmentDto { get; set; }
    }
}
