using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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

        }

        public static User CreateUser(UserDto userDto)
        {
            Guid UserId = Guid.NewGuid();
            DateTime createdOnUtc = DateTime.UtcNow;
            return new User(UserId, userDto.FirstName, userDto.LastName, userDto.Email, userDto.Password, createdOnUtc);
        }
    }
}
