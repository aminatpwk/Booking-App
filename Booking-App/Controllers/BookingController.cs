using Booking.Application.Common.DTOs.BookingDTOs;
using Booking.Application.Features.Bookings.Commands;
using Booking.Application.Features.Bookings.Queries.GetAll;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController, Authorize(Roles ="User")]
    public class BookingController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> Create([FromBody] CreateBookingDto createBookingDto)
        {
            var command = new CreateBookingCommand { CreateBookingDto = createBookingDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }

        [HttpGet("my")]
        public async Task<IActionResult> GetMyBookings()
        {
            var query = new GetAllBookingsQuery();
            var result = await _sender.Send(query);
            return Ok(result);
        }
    }
}
