using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booking.Domain.Reviews;
using Microsoft.AspNetCore.Authorization;
using Booking.Application.Features.Reviews.Commands.CreateReview;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class ReviewController(ISender _sender) : ControllerBase
    {
        [HttpPost, Authorize(Roles = "User")]
        public async Task<IResult> CreateReview([FromBody] ReviewDto reviewDto)
        {
            var command = new CreateReviewCommand { ReviewDto = reviewDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }
    }
}
