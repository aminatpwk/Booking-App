using Booking.Application.Features.Apartments.Queries.Get;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Commands.UpdateApartment
{
    public class UpdateApartmentCommand : IRequest<Guid>
    {
        public ApartmentDetailDto ApartmentDetailDto { get; set; }
    }
}
