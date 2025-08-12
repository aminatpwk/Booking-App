using Booking.Application.Common.DTOs.BookingDTOs;
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
        public CreateBookingDto CreateBookingDto { get; set; }
    }
}
