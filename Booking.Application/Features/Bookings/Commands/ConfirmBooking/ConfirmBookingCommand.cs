using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Bookings.Commands.ConfirmBooking
{
    public class ConfirmBookingCommand : IRequest<bool>
    {
        public string Token { get; set; }
        public ConfirmBookingCommand(string token)
        {
            Token = token;
        }
    }
}
