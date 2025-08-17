using MediatR;
using Booking.Domain.Users;
using System.ComponentModel.DataAnnotations;
using AutoMapper;

namespace Booking.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserHandler : IRequestHandler<CreateUserCommand, Guid>
    {
        private readonly IUserRepository _userRepository;
        private readonly IMapper _mapper;

        public CreateUserHandler(IUserRepository userRepository, IMapper mapper)
        {
            _userRepository = userRepository;
            _mapper = mapper;
        }

        public async Task<Guid> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            if(request?.UserDto == null)
            {
                throw new ArgumentNullException(nameof(request), "Request cannot be null!");
            }

            var isUniqueUser = await _userRepository.IsEmailUnique(request.UserDto.Email, cancellationToken);
            if (!isUniqueUser)
            {
                throw new ValidationException("User with this e-mail already exists!");
            }

            var user = User.CreateUser(
                request.UserDto.FirstName,
                request.UserDto.LastName,
                request.UserDto.Email,
                request.UserDto.Password
                );

            await _userRepository.Add(user);
            return user.Id;
        }
    }
}
