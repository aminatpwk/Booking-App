using MediatR;
using Booking.Application.Common.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Queries.GetAll
{
    public class GetAllApartmentsQuery : IRequest<List<ApartmentDto>>
    {

    }
}
