using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Users;


namespace Booking.Domain.Owners
{
    public class Owner
    {
        [Key]
        public Guid Id { get; private set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public string IdentityCardNumber { get; private set; }
        public string BankAccount { get; private set; }
        public string PhoneNumber { get; private set; }
    }
}
