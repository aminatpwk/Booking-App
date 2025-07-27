using MediatR;
using Booking.Domain.Users;
using System.ComponentModel.DataAnnotations;

namespace Booking.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly CreateUserValidations _validations;

        public CreateUserHandler(IUserRepository userRepository, CreateUserValidations validations)
        {
            _userRepository = userRepository;
            _validations = validations;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var isValidResult = await _validations.ValidateAsync(request, cancellationToken);
            if (!isValidResult.IsValid)
            {
                var errors = string.Join(", ", isValidResult.Errors.Select(e => e.ErrorMessage));
                throw new ValidationException($"Validation failed: {errors}");
            }

            var isUniqueUser = await _userRepository.IsEmailUnique(request.UserDto.Email, cancellationToken);
            if (!isUniqueUser)
            {
                throw new Exception("User with this e-mail already exists!");
            }

            var user = new User(
                id: Guid.NewGuid(),
                firstName: request.UserDto.FirstName,
                lastName: request.UserDto.LastName,
                email: request.UserDto.Email,
                password: BCrypt.Net.BCrypt.HashPassword(request.UserDto.Password, 13),
                createdOnUtc: DateTime.UtcNow
            );

            await _userRepository.Add(user);
            return user.Id;
        }
    }
}
