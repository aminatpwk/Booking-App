using Booking.Application.Features.Photos.Commands.DeletePhotos;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class PhotoController (ISender _sender) : ControllerBase
    {

        [HttpDelete("{id}"), Authorize(Roles = "Owner")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var command = new DeletePhotoCommand { Id = id };
            var result = await _sender.Send(command);
            return Ok(result);
        }
    }
}
