using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
                throw new ValidationException();
            }

            var isUniqueUser = await _userRepository.IsEmailUnique(request.UserDto.Email, cancellationToken);
            if(!isUniqueUser)
            {
                throw new Exception("User with this e-mail already exists!");
            }

            var password = request.UserDto.Password;
            request.UserDto.Password = BCrypt.Net.BCrypt.HashPassword(password, 13);

            var user = User.CreateUser(request.UserDto);

            await _userRepository.Add(user);

            return user.Id;

        }
    }
}
