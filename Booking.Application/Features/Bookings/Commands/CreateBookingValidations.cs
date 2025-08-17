using FluentValidation;


namespace Booking.Application.Features.Bookings.Commands
{
    public class CreateBookingValidations : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidations()
        {
            RuleFor(x => x.CreateBookingDto.ApartmentId).NotEmpty().WithMessage("ApartmentID is required!");
            RuleFor(x => x.CreateBookingDto.End).GreaterThan(x => x.CreateBookingDto.Start).WithMessage("End date must be after start date!");
            RuleFor(x => x.CreateBookingDto.Start).GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Start date cannot be in the past!");
        }
    }
}
