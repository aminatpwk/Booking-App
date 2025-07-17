using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Commands.User;

namespace Booking.Application.Validations.User
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email).NotEmpty().WithMessage("Email is a required field")
                .EmailAddress().WithMessage("E-mail must be a valid email address. E.g., email@example.com");

            RuleFor(x => x.Password).NotEmpty().WithMessage("Password is a required field")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long")
                .Matches(@"^(?=.*[a-z])(?=.*[A-Z])(?=.*\d)").WithMessage("Password must contain at least one lowercase letter," +
                "one uppercase letter, and one digit");

            RuleFor(x => x.FirstName).NotEmpty().WithMessage("First name is a required field")
                .MaximumLength(50).WithMessage("Name shouldn't be more than 50 characters long")
                .Matches("[a-z]").WithMessage("Name should contain letters only");

            RuleFor(x => x.LastName).NotEmpty().WithMessage("Last name is a required field")
                .MaximumLength(50).WithMessage("Last name shouldn't be more than 50 characters long")
                .Matches("[a-z]").WithMessage("Last name should contain letters only");
        }
    }
}
