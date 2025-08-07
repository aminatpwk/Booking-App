using System.ComponentModel.DataAnnotations;


namespace Booking.Application.Features.Users.Login
{
    public record LoginUserDto
    {
        [Required]
        public string Email { get; init; }

        [Required]
        public string Password { get; init; }
        [Required]
        public string Role { get; init; }
    }
}
