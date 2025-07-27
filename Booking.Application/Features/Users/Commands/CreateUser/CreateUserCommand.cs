using Booking.Domain.Users;
using MediatR;

namespace Booking.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserCommand : IRequest<Guid>
    {
        public UserDto UserDto { get; set; }
    }
}
