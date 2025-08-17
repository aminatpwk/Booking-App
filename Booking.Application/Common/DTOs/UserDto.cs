using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs
{
    public class UserDto
    {
        [Required]
        public string FirstName { get; init; }
        [Required]
        public string LastName { get; init; }
        [Required]
        public string Email { get; init; }
        [Required]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
