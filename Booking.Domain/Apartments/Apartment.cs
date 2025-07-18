using Booking.Domain.Owners;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Apartments
{
    public class Apartment
    {
        [Key]
        public Guid Id { get; set; }
        public string Name { get; private set; }
        public string Address { get; private set; }
        public decimal Price { get; private set; }
        public string Decription { get; private set; }
        public decimal CleaningFee { get; private set; }
        public DateTime? LastBookedOnUtc { get; private set; }

        public virtual ICollection<Amenity> Amenities { get; private set; } = [];
        public virtual ICollection<Owner> Owners { get; } = [];
    }
}
