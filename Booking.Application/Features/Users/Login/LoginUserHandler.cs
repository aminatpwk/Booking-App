using Booking.Application.Features.Users.Auth;
using MediatR;
using Booking.Domain.Users;

namespace Booking.Application.Features.Users.Login
{
    public class LoginUserHandler : IRequestHandler<LoginUserCommand, string>
    {
        private readonly IAuthService _authService;
        private readonly IUserRepository _userRepository;
        public LoginUserHandler(IAuthService authService, IUserRepository userRepository)
        {
            _authService = authService;
            _userRepository = userRepository;
        }

        public async Task<string> Handle(LoginUserCommand request, CancellationToken cancellationToken)
        {
            var loggedUserDto = request.LoginUserDto;
            if (loggedUserDto is null)
            {
                throw new Exception("User cannot be null");
            }

            User user = await _userRepository.GetUserByEmail(loggedUserDto.Email, cancellationToken);
            if (user is null)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }


            var isCorrectPassword = BCrypt.Net.BCrypt.Verify(loggedUserDto.Password, user.Password);
            if (!isCorrectPassword)
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            if (loggedUserDto.Role == "Owner" && user.Owner == null)
            {
                throw new UnauthorizedAccessException("You do not have Owner role.");
            }

            var token = await _authService.GenerateToken(user, loggedUserDto.Role);

            return token;
        }
    }
}
