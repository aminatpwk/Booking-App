using Booking.Application.Features.Users.Commands.CreateUser;
using Booking.Application.Common.DTOs;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Booking.Application.Features.Users.Login;
using Booking.Application.Features.Users.Commands.DeleteUser;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class UserController(ISender _sender) : ControllerBase
    {
        [HttpPost("register")]
        public async Task<IResult> Register([FromBody] UserDto userDto)
        {
            var command = new CreateUserCommand { UserDto = userDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }

        [HttpPost("login")]
        public async Task<IResult> Login([FromBody] LoginUserDto loginUserDto)
        {
            var command = new LoginUserCommand { LoginUserDto = loginUserDto };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }

        [HttpDelete("{id}")]
        public async Task<IResult> DeleteUser(Guid id)
        {
            var command = new DeleteUserCommand { Id = id };
            var result = await _sender.Send(command);
            return Results.Ok(result);
        }
    }
}
