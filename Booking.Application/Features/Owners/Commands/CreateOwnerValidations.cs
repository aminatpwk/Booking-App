using FluentValidation;

namespace Booking.Application.Features.Owners.Commands
{
    public class CreateOwnerValidations : AbstractValidator<CreateOwnerCommand>
    {

        public CreateOwnerValidations() {
            RuleFor(x => x.OwnerDto.UserId).NotEmpty().WithMessage("Owner should be a registered user!");

            RuleFor(x => x.OwnerDto.IdentityCardNumber).NotEmpty().WithMessage("Identity card number is a required field.")
                .Must(IsValidIdentityCard).WithMessage("Identity card number format is invalid");

            RuleFor(x => x.OwnerDto.BankAccount)
                .NotEmpty().WithMessage("Bank account is a required field.")
                .Matches(@"^\d{16}$").WithMessage("Bank account must be exactly 16 digits.");

            RuleFor(x => x.OwnerDto.PhoneNumber).NotEmpty().WithMessage("Phone number is a required field.")
                .Matches(@"^\d{10}$").WithMessage("Phone number must be exactly 10 digits.");
        }

        private bool IsValidIdentityCard(string identitycardno)
        {
            if (string.IsNullOrWhiteSpace(identitycardno))
            {
                return false;
            }

            if (identitycardno.Length != 10)
            {
                return false;
            }

            if (!identitycardno.All(char.IsDigit))
            {
                return false;
            }

            var yearString = identitycardno.Substring(0, 2);
            var monthString = identitycardno.Substring(2, 2);
            var dayString = identitycardno.Substring(4, 2);
            var sequenceString = identitycardno.Substring(6, 3);
            var checkDigitString = identitycardno.Substring(9, 1);

            if (!int.TryParse(yearString, out var year))
            {
                return false;
            }
            if (!int.TryParse(monthString, out var month))
            {
                return false;
            }
            if (!int.TryParse(dayString, out var day))
            {
                return false;
            }
            if (!int.TryParse(sequenceString, out var sequence))
            {
                return false;
            }
            if (!int.TryParse(checkDigitString, out var checkDigit))
            {
                return false;
            }

            if (!((month >= 1 && month <= 12) || (month >= 51 && month <= 62)))
            {
                return false;
            }

            if (day < 1 || day > 31)
            {
                return false;
            }

            var actualMonth = month > 50 ? month - 50 : month;
            var fullYear = year <= 30 ? 2000 + year : 1900 + year;

            try
            {
                var birthDate = new DateTime(fullYear, actualMonth, day);

                if (birthDate > DateTime.UtcNow)
                {
                    return false;
                }
                if (birthDate < new DateTime(1900, 1, 1))
                {
                    return false;
                }
            }
            catch (ArgumentOutOfRangeException ex)
            {
                return false;
            }

            var calculatedCheckDigit = CalculateCheckDigit(identitycardno.Substring(0, 9));
            bool isValid = calculatedCheckDigit == checkDigit;

            return isValid;
        }

        private int CalculateCheckDigit(string identitycardno)
        {

            var sum = 0;
            var weights = new int[] { 1, 2, 3, 4, 5, 6, 7, 8, 9 };

            for (int i = 0; i < 9; i++)
            {
                sum += int.Parse(identitycardno[i].ToString()) * weights[i];
            }

            return sum % 10;
        }
    }
}
