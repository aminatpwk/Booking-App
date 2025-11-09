using Booking.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs
{
    public class ApartmentDto
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public string Name { get; init; }
        public string Country { get; init; }
        public string City { get; init; }
        public string Address { get; init; }
        public decimal Price { get; init; }
        public string Description { get; init; }
        public decimal CleaningFee { get; init; }
        public int Bedrooms { get; init; }
        public int Bathrooms { get; init; }
        public int MaxGuests { get; init; }
        public ApartmentType Type { get; init; }
        public List<Amenity> Amenities { get; init; } = [];
        public bool IsActive { get; init; } = true;
        public bool IsAvailable { get; init; } = true;

        [MinLength(4, ErrorMessage = "At least 4 property images should be uploaded!")]
        public IReadOnlyList<string> ImagesBase64 { get; init; } = new List<string>();
    }
}
