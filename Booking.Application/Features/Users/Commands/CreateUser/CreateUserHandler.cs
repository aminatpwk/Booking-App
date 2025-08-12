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
            var isUniqueUser = await _userRepository.IsEmailUnique(request.UserDto.Email, cancellationToken);
            if (!isUniqueUser)
            {
                throw new Exception("User with this e-mail already exists!");
            }

            var user = _mapper.Map<User>(request.UserDto);
            user.SetPassword(request.UserDto.Password);

            await _userRepository.Add(user);
            return user.Id;
        }
    }
}
