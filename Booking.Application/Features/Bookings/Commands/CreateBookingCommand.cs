using Booking.Application.Common.DTOs;
using Booking.Domain.Bookings;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Bookings.Commands
{
    public class CreateBookingCommand : IRequest<Guid>
    {
        public BookingDto BookingDto { get; set; }
    }
}
