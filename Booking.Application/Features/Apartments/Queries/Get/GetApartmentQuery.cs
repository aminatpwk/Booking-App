using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Queries.Get
{
    public class GetApartmentQuery : IRequest<ApartmentDetailDto>
    {
        public Guid Id { get; set; }
    }
}
