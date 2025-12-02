
using Booking.Application.Common.DTOs;
using Booking.Application.Features.Users;
using MediatR;

namespace Booking.Application.Features.Owners.Queries.GetOwnerProfile
{
    public class GetOwnerProfileQueryHandler : IRequestHandler<GetOwnerProfileQuery, OwnerDto?>
    {
        private readonly IOwnerRepository _ownerRepository;
        private readonly ICurrentUserService _currentUserService;
        public GetOwnerProfileQueryHandler(IOwnerRepository ownerRepository, ICurrentUserService currentUserService)
        {
            _ownerRepository = ownerRepository;
            _currentUserService = currentUserService;
        }

        public async Task<OwnerDto?> Handle(GetOwnerProfileQuery request, CancellationToken cancellationToken)
        {
            var userId = _currentUserService.UserId;
            if (userId == Guid.Empty)
            {
                throw new UnauthorizedAccessException("User not authenticated");
            }

            var owner = await _ownerRepository.GetByUserId(userId);
            if (owner == null)
            {
                return null;
            }
            return new OwnerDto
            {
                UserId = owner.UserId,
                IdentityCardNumber = owner.IdentityCardNumber,
                BankAccount = owner.BankAccount,
                PhoneNumber = owner.PhoneNumber
            };
        }
    }
}
