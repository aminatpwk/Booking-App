using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Users;

namespace Booking.Application.Responses.User
{
    public class RegisterUserResponse
    {
        public Guid UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedOnUtc { get; set; }


        public static Task<Guid> Success(Booking.Domain.Users.User user)
        {
            return new Guid();
            
        }
    }
}
