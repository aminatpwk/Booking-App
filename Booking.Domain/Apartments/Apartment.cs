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
        public Apartment(Guid id, string name, string address, decimal price, string description, decimal cleaningFee, List<Amenity> amenities)
        {
            Id = id;
            Name = name;
            Address = address;
            Price = price;
            Decription = description;
            CleaningFee = cleaningFee;
            Amenities = amenities;
        }

        public Apartment()
        {

        }

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

        public static Apartment Create(ApartmentDto apartmentdto, Owner owner)
        {
            var id = Guid.NewGuid();
            var apartment = new Apartment(
                id,
                apartmentdto.Name,
                apartmentdto.Address,
                apartmentdto.Price,
                apartmentdto.Description,
                apartmentdto.CleaningFee,
                apartmentdto.Amenities);
            apartment.Owners.Add(owner);
            return apartment;
        }
    }
}
