using Booking.Application.Features.Users;
using Moq;
using AutoMapper;
using Booking.Application.Features.Users.Commands.CreateUser;
using Xunit;
using Booking.Application.Common.DTOs;
using System.ComponentModel.DataAnnotations;

namespace Booking.Tests.Unit.Application.Commands.User
{
    public class CreateUserCommandTests
    {
        private readonly Mock<IUserRepository> _userRepositoryMock;
        private readonly Mock<IMapper> _mapperMock;
        private readonly CreateUserHandler _handler;

        public CreateUserCommandTests()
        {
            _userRepositoryMock = new Mock<IUserRepository>();
            _mapperMock = new Mock<IMapper>();
            _handler = new CreateUserHandler(_userRepositoryMock.Object, _mapperMock.Object);
        }

        [Fact]
        public async Task Handle_WithNullRequest_ShouldThrowArgumentNullException()
        {
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(null, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithNullUserDto_ShouldThrowArgumentNullException()
        {
            var command = new CreateUserCommand { UserDto = null };
            await Assert.ThrowsAsync<ArgumentNullException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithExistingEmail_ShouldThrowValidationException()
        {
            var userDto = new UserDto { FirstName = "TestName", LastName = "TestLastName", Email = "testemail@example.com", Password = "!Test123" };
            var command = new CreateUserCommand { UserDto = userDto };
            _userRepositoryMock.Setup(repo => repo.IsEmailUnique(userDto.Email, It.IsAny<CancellationToken>())).ReturnsAsync(false);
            await Assert.ThrowsAsync<ValidationException>(() => _handler.Handle(command, CancellationToken.None));
        }

        [Fact]
        public async Task Handle_WithValidData_ShouldCreateUserAndReturnId()
        {
            var userDto = new UserDto { FirstName = "TestName", LastName = "TestLastName", Email = "testemail@example.com", Password = "!Test123" };
            var command = new CreateUserCommand { UserDto = userDto };
            var user = Booking.Domain.Users.User.CreateUser(userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password);
            _userRepositoryMock.Setup(repo => repo.IsEmailUnique(userDto.Email, It.IsAny<CancellationToken>())).ReturnsAsync(true);
            _userRepositoryMock.Setup(repo => repo.Add(It.IsAny<Booking.Domain.Users.User>())).ReturnsAsync(user);

            var result = await _handler.Handle(command, CancellationToken.None);
            Assert.NotEqual(Guid.Empty, result);
            _userRepositoryMock.Verify(repo => repo.Add(It.Is<Booking.Domain.Users.User>(u => u.Email == userDto.Email)), Times.Once);
        }
    }
}
