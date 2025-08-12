using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Booking.Domain.Users;
using Booking.Domain.Apartments;

namespace Booking.Domain.Owners
{
    public class Owner
    {
        public Owner()
        {

        }

        public Owner(Guid id, Guid userId, string identityCardNumber, string bankAccount, string phoneNumber)
        {
            Id = id;
            UserId = userId;
            IdentityCardNumber = identityCardNumber;
            BankAccount = bankAccount;
            PhoneNumber = phoneNumber;
        }

        [Key]
        public Guid Id { get; private set; }

        [ForeignKey(nameof(User))]
        public Guid UserId { get; private set; }
        public User User { get; private set; }
        public string IdentityCardNumber { get; private set; }
        public string BankAccount { get; private set; }
        public string PhoneNumber { get; private set; }
        public List<Apartment> Apartments { get; private set; } = [];

        public static Owner CreateOwner(Guid userId, string idcardno, string bankacc, string phoneno)
        {
            Guid OwnerId = Guid.NewGuid();
            return new Owner(OwnerId, userId, idcardno, bankacc, phoneno);
        }

        //public void SetApartments(List<Apartment> apartments)
        //{
        //    Apartments = apartments;
        //}

    }
}
