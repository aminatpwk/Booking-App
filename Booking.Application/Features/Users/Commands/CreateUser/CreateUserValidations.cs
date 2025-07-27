using FluentValidation;

namespace Booking.Application.Features.Users.Commands.CreateUser
{
    public class CreateUserValidations : AbstractValidator<CreateUserCommand>
    {
        public CreateUserValidations()
        {
            RuleFor(x => x.UserDto.FirstName).NotEmpty().WithMessage("First name is a required field.")
                .MaximumLength(50).WithMessage("First name cannot exceed 50 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("First name can only contain letters.");

            RuleFor(x => x.UserDto.LastName).NotEmpty().WithMessage("Last name is a required field.")
                .MaximumLength(50).WithMessage("Last name cannot exceed 50 characters.")
                .Matches("^[a-zA-Z]+$").WithMessage("Last name can only contain letters.");

            RuleFor(x => x.UserDto.Email).NotEmpty().WithMessage("Email is a required field.")
                .EmailAddress().WithMessage("Invalid email format.")
                .MaximumLength(100).WithMessage("Email cannot exceed 100 characters.");

            RuleFor(x => x.UserDto.Password).NotEmpty().WithMessage("Password is a required field.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)(?=.*[@$!%*?&])[A-Za-z\d@$!%*?&]+$").WithMessage("Password must contain at least one uppercase letter, one lowercase letter, one number, and one special character.");

        }
    }
}
