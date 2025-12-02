using Booking.Domain.Owners;
using Booking.Application.Repositories;

namespace Booking.Application.Features.Owners
{
    public interface IOwnerRepository : IGenericRepository<Owner>
    {
        Task<bool> IsUniqueIdentityCardNumber(string identitycardno, CancellationToken cancellationToken);
        Task<Owner?> GetByUserId(Guid userId);
        Task<bool> UserAlreadyHasOwnerProfile(Guid userId, CancellationToken cancellationToken);
    }
}
