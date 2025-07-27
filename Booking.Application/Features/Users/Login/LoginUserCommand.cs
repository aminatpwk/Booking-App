using MediatR;

namespace Booking.Application.Features.Users.Login
{
    public class LoginUserCommand : IRequest<string>
    {
        public LoginUserDto LoginUserDto { get; set; }
    }
}
