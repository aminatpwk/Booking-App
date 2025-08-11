using Booking.Application.Common.DTOs;
using Booking.Application.Features.Bookings.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class BookingController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> Create([FromBody] BookingDto bookingDto)
        {
            var command = new CreateBookingCommand { BookingDto = bookingDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }
    }
}
