using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Commands.DeleteApartment
{
    public class DeleteApartmentCommand : IRequest<Guid>
    {
        public Guid Id { get; set; }
    }
}
