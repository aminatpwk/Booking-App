using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Domain.Users;
using Booking.Infrastructure;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(ISender _sender) : ControllerBase
    {
        

        [HttpPost]
        public async Task<IResult> Register([FromBody] UserDto userDto)
        {
            var command = new CreateUserCommand { UserDto = userDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }
    }
}
