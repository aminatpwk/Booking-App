using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Commands.CreateApartment
{
    public class CreateApartmentValidations : AbstractValidator<CreateApartmentCommand>
    {
        public CreateApartmentValidations() { 
            RuleFor(x => x.ApartmentDto.Name)
                .NotEmpty().WithMessage("Apartment name is required.")
                .MaximumLength(100).WithMessage("Apartment name must not exceed 100 characters.");
            RuleFor(x => x.ApartmentDto.Description)
                .NotEmpty().WithMessage("Apartment description is required.")
                .MaximumLength(500).WithMessage("Apartment description must not exceed 500 characters.");
            RuleFor(x => x.ApartmentDto.Price)
                .GreaterThan(0).WithMessage("Price per night must be greater than zero.");
            RuleFor(x => x.ApartmentDto.Address)
                .NotEmpty().WithMessage("Apartment address is required.")
                .MaximumLength(200).WithMessage("Apartment address must not exceed 200 characters.");
            RuleFor(x => x.ApartmentDto.ImagesBase64).NotNull().WithMessage("You must upload images for the apartment!").Must(images => images != null && images.Count >= 4).WithMessage("You should upload at least 4 images per property!");
        }
    }
}
