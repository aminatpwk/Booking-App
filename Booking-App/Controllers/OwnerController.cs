using Booking.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Booking.Application.Features.Owners.Commands;
namespace Booking_App.Controllers
{
    [ApiController]
    [Route("api/v1/[controller]")]
    public class OwnerController(ISender _sender) : ControllerBase
    {
        [HttpPost]
        public async Task<IResult> CreateOwner([FromBody] OwnerDto ownerDto)
        {
            var command = new CreateOwnerCommand { OwnerDto = ownerDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);  
        }

       
    }
}
