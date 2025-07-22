using Booking.Domain.Apartments;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Commands.CreateApartment
{
    public class CreateApartmentCommand : IRequest<Guid>
    {
        public ApartmentDto ApartmentDto { get; set; }
    }
}
