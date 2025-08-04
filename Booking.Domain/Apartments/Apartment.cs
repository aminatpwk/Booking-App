using Booking.Domain.Apartments;
using Booking.Domain.Owners;
using Booking.Domain.Photos;
using System.ComponentModel.DataAnnotations;

public class Apartment
{
    public Apartment(Guid id, string name, string address, decimal price, string description, decimal cleaningFee, List<Amenity> amenities, Guid ownerId)
    {
        Id = id;
        Name = name;
        Address = address;
        Price = price;
        Decription = description;
        CleaningFee = cleaningFee;
        Amenities = amenities;
        OwnerId = ownerId;
    }

    public Apartment() { }

    [Key]
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Address { get; private set; }
    public decimal Price { get; private set; }
    public string Decription { get; private set; }
    public decimal CleaningFee { get; private set; }
    public DateTime? LastBookedOnUtc { get; private set; }

    public virtual ICollection<Amenity> Amenities { get; set; } = [];
    public virtual ICollection<Photo> Photos { get; set; } = [];

    public Guid OwnerId { get; set; }
    public Owner Owner { get; set; }

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
            apartmentdto.Amenities,
            owner.Id);
        return apartment;
    }

    public void UpdateApartment(string name, string address, decimal price, string description, decimal cleaningfee, List<Amenity> amenities)
    {
        Name = name;
        Address = address;
        Price = price;
        Decription = description;
        CleaningFee = cleaningfee;
        Amenities = amenities;
    }
}
