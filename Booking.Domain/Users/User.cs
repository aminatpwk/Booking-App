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

        public User()
        {
        }

        public User(Guid id, string firstName, string lastName, string email, string password, DateTime createdOnUtc)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Email = email;
            Password = password;
            CreatedOnUtc = createdOnUtc;
        }

        public static User CreateUser(string firstName, string lastName, string email, string password)
        {
            Guid UserId = Guid.NewGuid();
            DateTime createdOnUtc = DateTime.UtcNow;
            return new User(UserId, firstName, lastName, email, password ,createdOnUtc);
        }

        public void SetPassword(string plainPassword)
        {
            Password = BCrypt.Net.BCrypt.HashPassword(plainPassword);
        }
    }
}
