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
            var isUnique = await _context.Owners.Where(o => o.IdentityCardNumber == identitycardno).ToListAsync(cancellationToken);
            return !isUnique.Any();
        }
    }
}
