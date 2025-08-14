using Booking.Application.Common.DTOs.BookingDTOs;
using Booking.Application.Features.Bookings.Commands;
using Booking.Application.Features.Bookings.Commands.CancelBooking;
using Booking.Application.Features.Bookings.Commands.ConfirmBooking;
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

        [HttpGet("confirm/{token}")]
        public async Task<IActionResult> ConfirmBooking(string token)
        {
            try
            {
                var command = new ConfirmBookingCommand(token);
                var result = await _sender.Send(command);
                if (result)
                {
                    return Ok(new { message = "Booking confirmed successfully!" });
                }

                return BadRequest(new { message = "Failed to confirm booking." }); ;
            }catch(ArgumentException ex)
            {
                return NotFound(new {message = ex.Message});
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while confirming booking" });
            }
        }

        [HttpGet("cancel/{token}")]
        public async Task<IActionResult> CancelBooking(string token)
        {
            try
            {
                var command = new CancelBookingCommand(token);
                var result = await _sender.Send(command);
                if (result)
                {
                    return Ok(new { message = "Booking cancelled successfully!" });
                }

                return BadRequest(new { message = "Failed to cancel booking!" });
            }catch(ArgumentException ex)
            {
                return NotFound(new { message = ex.Message });
            }
            catch (InvalidOperationException ex)
            {
                return BadRequest(new { message = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = "An error occurred while rejecting booking" });
            }
        }
    }
}
