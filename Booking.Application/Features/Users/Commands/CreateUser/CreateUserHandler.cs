using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Users;
namespace Booking.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;

        public CreateUserHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var isUniqueUser = await _userRepository.IsEmailUnique(request.UserDto.Email, cancellationToken);
            if(!isUniqueUser)
            {
                throw new Exception("User with this e-mail already exists!");
            }

            var password = request.UserDto.Password;
            var user = User.CreateUser(request.UserDto);

            await _userRepository.Add(user);

            return user.Id;

        }
    }
}
