using Xunit;
using Booking.Domain.Users;

namespace Booking.Tests.Unit.Domain
{
    public class UserTests
    {
        private User CreateDefaultTestingUser(string firstName="TestName", string lastName = "TestLastName", string email="testemail@example.com", string password = "!Test123")
        {
            return User.CreateUser(firstName, lastName, email, password);
        }

        [Fact]
        public void CreateUser_ShouldInitializePropertiesCorrectly()
        {
            var user = CreateDefaultTestingUser();
            Assert.NotEqual(Guid.Empty, user.Id);
            Assert.Equal("TestName", user.FirstName);
            Assert.Equal("TestLastName", user.LastName);
            Assert.Equal("testemail@example.com", user.Email);
            Assert.NotEqual("!Test123", user.Password);
            Assert.True(BCrypt.Net.BCrypt.Verify("!Test123", user.Password));
            Assert.True(user.CreatedOnUtc <= DateTime.UtcNow);
        }

        [Fact]
        public void VerifyPassword_WithCorrectPass_ShouldReturnTrue()
        {
            var user = CreateDefaultTestingUser(password: "CorrectPass123!");
            bool isValid = user.VerifyPassword("CorrectPass123!");
            Assert.True(isValid);
        }

        [Fact]
        public void VerifyPassword_WithIncorrectPass_ShouldReturnFalse()
        {
            var user = CreateDefaultTestingUser(password: "CorrectPass123!");
            bool isValid = user.VerifyPassword("WrongPass123!");
            Assert.False(isValid);
        }
    }
}
