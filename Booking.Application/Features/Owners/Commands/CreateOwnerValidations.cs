using FluentValidation;

namespace Booking.Application.Features.Owners.Commands
{
    public class CreateOwnerValidations : AbstractValidator<CreateOwnerCommand>
    {

        public CreateOwnerValidations() {
            RuleFor(x => x.OwnerDto.UserId).NotEmpty().WithMessage("Owner should be a registered user!");

            RuleFor(x => x.OwnerDto.IdentityCardNumber).NotEmpty().WithMessage("Identity card number is a required field.")
                .Must(IsValidIdentityCard).WithMessage("Identity card number format is invalid based on the Albanian standard.");

            RuleFor(x => x.OwnerDto.BankAccount)
                .NotEmpty().WithMessage("Bank account is a required field.")
                .Matches(@"^\d{16}$").WithMessage("Bank account must be exactly 16 digits.");

            RuleFor(x => x.OwnerDto.PhoneNumber).NotEmpty().WithMessage("Phone number is a required field.")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be exactly 10 digits.");
        }

        private bool IsValidIdentityCard(string identitycardno)
        {
            if (string.IsNullOrWhiteSpace(identitycardno) || identitycardno.Length != 10)
            {
                return false;
            }

            char genderCode = identitycardno[0];
            if(!"JFEK".Contains(genderCode))
            {
                return false;
            }

            var digitsPart = identitycardno.Substring(1, 8);
            if(!digitsPart.All(char.IsDigit))
            {
                return false;
            }

            char checkChar = identitycardno[9];
            if(!char.IsLetter(checkChar))
            {
                return false;
            }

            return true;
        }

    }
}
