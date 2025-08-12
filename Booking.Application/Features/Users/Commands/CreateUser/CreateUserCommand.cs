using MediatR;
using Booking.Application.Common.DTOs;

namespace Booking.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public UserDto UserDto { get; set; }
    }
}
