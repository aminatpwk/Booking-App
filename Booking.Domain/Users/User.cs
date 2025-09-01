using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BCrypt.Net;
using Booking.Domain.Owners;

namespace Booking.Domain.Users
{
    public class User
    {
        [Key]
        public Guid Id { get; set; }
        public string FirstName { get; private set; } 
        public string LastName { get; private set; }  
        public string Email { get; private set; } 
        public string Password { get; private set; } 
        public DateTime CreatedOnUtc { get; private set; }
        public Owner? Owner { get; private set; }

        private User()
        {
        }

        public static User CreateUser(string firstName, string lastName, string email, string plainPassword)
        {
            return new User
            {
                Id = Guid.NewGuid(),
                FirstName = firstName,
                LastName = lastName,
                Email = email,
                Password = BCrypt.Net.BCrypt.HashPassword(plainPassword),
                CreatedOnUtc = DateTime.UtcNow
            };
        }

        public bool VerifyPassword(string plainPassword)
        {
            return BCrypt.Net.BCrypt.Verify(plainPassword, Password);
        }
    }
}
