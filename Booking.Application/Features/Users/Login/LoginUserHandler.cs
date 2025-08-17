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
            if(request?.LoginUserDto == null)
            {
                throw new ArgumentNullException(nameof(request), "Login request cannot be null!");
            }

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

            if (!user.VerifyPassword(loggedUserDto.Password))
            {
                throw new UnauthorizedAccessException("Invalid email or password");
            }

            if (loggedUserDto.Role.Equals("Owner", StringComparison.OrdinalIgnoreCase) && user.Owner == null)
            {
                throw new UnauthorizedAccessException("You do not have Owner role.");
            }

            var token = await _authService.GenerateToken(user, loggedUserDto.Role);

            return token;
        }
    }
}
