using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FluentValidation;

namespace Booking.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewValidations : AbstractValidator<CreateReviewCommand>
    {
        public CreateReviewValidations()
        { 
            RuleFor(x => x.ReviewDto.ApartmentId)
                .NotEmpty().WithMessage("Apartment ID is required.");
            RuleFor(x => x.ReviewDto.Rating)
                .InclusiveBetween(1, 5).WithMessage("Rating must be between 1 and 5.");
            RuleFor(x => x.ReviewDto.Comment)
                .NotEmpty().WithMessage("Comment is required.")
                .MaximumLength(500).WithMessage("Comment must not exceed 500 characters.");
        }
    }
}
