using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Application.Features.Owners;
using Booking.Domain.Owners;
using Booking.Infrastructure.GenericRepoImpl;
using MediatR.NotificationPublishers;
using Microsoft.EntityFrameworkCore;

namespace Booking.Infrastructure.Owners
{
    public class OwnerRepository : GenericRepository<Owner>,IOwnerRepository
    {
        private readonly BookingContext _context;
        public OwnerRepository(BookingContext context) : base(context)
        {
            _context = context;
        }

        public async Task<bool> IsUniqueIdentityCardNumber(string identitycardno, CancellationToken cancellationToken)
        {
            if (string.IsNullOrWhiteSpace(identitycardno))
            {
                return false;
            }

            var isUnique = await _context.Owners.AnyAsync(o => o.IdentityCardNumber.ToLower() == identitycardno.ToLower(), cancellationToken);
            return !isUnique;
        }

        public async Task<Owner?> GetByUserId(Guid userId)
        {
            return await _context.Owners.Include(o => o.User).FirstOrDefaultAsync(o => o.UserId == userId);
        }

        public async Task<bool> UserAlreadyHasOwnerProfile(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.Owners.AnyAsync(o => o.UserId == userId, cancellationToken);
        }
    }
}
