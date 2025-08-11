using MediatR;
using Booking.Domain.Reviews;
using Booking.Application.Features.Users;

namespace Booking.Application.Features.Reviews.Commands.CreateReview
{
    public class CreateReviewHandler : IRequestHandler<CreateReviewCommand, Guid>
    {
        private readonly IReviewRepository _reviewRepository;
        private readonly CreateReviewValidations _reviewValidations;
        private readonly ICurrentUserService _currentUserService;

        public CreateReviewHandler(IReviewRepository reviewRepository, CreateReviewValidations reviewValidations, ICurrentUserService currentUserService)
        {
            _reviewRepository = reviewRepository;
            _reviewValidations = reviewValidations;
            _currentUserService = currentUserService;
        }

        public async Task<Guid> Handle(CreateReviewCommand request, CancellationToken cancellationToken)
        {
            var isValidReview = await _reviewValidations.ValidateAsync(request, cancellationToken);
            if(!isValidReview.IsValid)
            {
                throw new Exception("Invalid review data.");
            }

            var currentUserId = _currentUserService.UserId;

            var review = Review.Create(request.ReviewDto.ApartmentId, currentUserId, request.ReviewDto.Rating, request.ReviewDto.Comment);
            await _reviewRepository.Add(review);
            return review.Id;
        }

    }
}
