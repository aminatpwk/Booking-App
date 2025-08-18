using Booking.Domain.Apartments;
using Booking.Domain.Owners;
using Booking.Domain.Photos;
using Booking.Domain.Reviews;
using System.ComponentModel.DataAnnotations;

public class Apartment
{
    public Apartment(
        Guid id, 
        string name, 
        string country, 
        string city ,
        string address, 
        decimal price, 
        string description, 
        decimal cleaningFee, 
        int bedrooms,
        int bathrooms,
        int maxGuests,
        ApartmentType type,
        List<Amenity> amenities, 
        bool isActive,
        bool isAvailable,
        Guid ownerId)
    {
        Id = id;
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Price = price;
        Decription = description;
        CleaningFee = cleaningFee;
        Bedrooms = bedrooms;
        Bathrooms = bathrooms;
        MaxGuests = maxGuests;
        Type = type;
        Amenities = amenities;
        IsActive = isActive;
        IsAvailable = isAvailable;
        OwnerId = ownerId;
    }

    public Apartment() { }

    [Key]
    public Guid Id { get; set; }
    public string Name { get; private set; }
    public string Country { get; private set; }
    public string City { get; private set; }
    public string Address { get; private set; }
    public decimal Price { get; private set; }
    public string Decription { get; private set; }
    public decimal CleaningFee { get; private set; }
    public DateTime? LastBookedOnUtc { get; private set; }
    
    public int Bedrooms { get; set; }
    public int Bathrooms { get; set; }
    public int MaxGuests { get; set; }
    public ApartmentType Type { get; private set; }
    public virtual ICollection<Amenity> Amenities { get; set; } = [];
    public virtual ICollection<Photo> Photos { get; set; } = [];
    public virtual ICollection<Review>? Reviews { get; set; } = [];
    public bool IsActive { get; set; } = true;
    public bool IsAvailable { get; set; } = true;
    

    public Guid OwnerId { get; set; }
    public Owner Owner { get; set; }

    public static Apartment Create(Guid ownerId, string name, string country, string city, string address, decimal price, string description, decimal cleaningFee, int bedrooms, int bathrooms, int maxGuests, ApartmentType type, List<Amenity> amenities, bool isActive, bool isAvailable)
    {
        var id = Guid.NewGuid();
        return new Apartment(
            id,
            name,
            country,
            city,
            address,
            price,
            description,
            cleaningFee,
            bedrooms,
            bathrooms,
            maxGuests,
            type,
            amenities,
            isActive,
            isAvailable,
            ownerId
        );
    }


    public void UpdateApartment(string name, string country, string city, string address, decimal price, string description, decimal cleaningfee, int bedrooms, int bathrooms, int maxGuests, ApartmentType type, List<Amenity> amenities, bool isActive, bool isAvailable)
    {
        Name = name;
        Country = country;
        City = city;
        Address = address;
        Price = price;
        Decription = description;
        CleaningFee = cleaningfee;
        Bedrooms = bedrooms;
        Bathrooms = bathrooms;
        MaxGuests = maxGuests;
        Type = type;
        Amenities = amenities;
        IsActive = isActive;
        IsAvailable = isAvailable;
    }

    public void SetLastBookedOnUtc(DateTime bookingDate)
    {
        LastBookedOnUtc = bookingDate;
    }
}
