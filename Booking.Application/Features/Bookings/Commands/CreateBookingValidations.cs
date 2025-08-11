using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Booking.Application.Features.Bookings.Commands
{
    public class CreateBookingValidations : AbstractValidator<CreateBookingCommand>
    {
        public CreateBookingValidations()
        {
            RuleFor(x => x.BookingDto.ApartmentId).NotEmpty().WithMessage("ApartmentID is required!");
            RuleFor(x => x.BookingDto.Start).LessThan(x => x.BookingDto.End).WithMessage("Start date must be before end date!");
            RuleFor(x => x.BookingDto.End).GreaterThan(x => x.BookingDto.Start).WithMessage("End date must be after start date!");
            RuleFor(x => x.BookingDto.Start).GreaterThanOrEqualTo(DateTime.UtcNow.Date).WithMessage("Start date cannot be in the past!");
        }
    }
}
