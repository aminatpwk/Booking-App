using Booking.Application.Common.DTOs;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Bookings.Queries.GetAll
{
    public class GetAllBookingsQuery : IRequest<List<BookingDto>>
    {

    }
}
