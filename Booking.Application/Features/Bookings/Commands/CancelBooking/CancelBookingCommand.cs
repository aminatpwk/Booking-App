using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Bookings.Commands.CancelBooking
{
    public class CancelBookingCommand : IRequest<bool>
    {
        public string Token { get; set; }
        public CancelBookingCommand(string token)
        {
            Token = token;
        }
    }
}
