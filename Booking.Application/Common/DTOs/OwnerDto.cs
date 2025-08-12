using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs
{
    public class OwnerDto
    {
        public Guid UserId { get; init; }
        public string IdentityCardNumber { get; init; }
        public string BankAccount { get; init; }
        public string PhoneNumber { get; init; }
        //public List<Guid>? ApartmentId { get; init; }
    }
}
