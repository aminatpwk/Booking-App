using Booking.Domain.Apartments;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Features.Apartments.Queries.Get
{
    public class ApartmentDetailDto : ApartmentDto
    {
        public ApartmentDetailDto() { }
        public string Address { get; init; } 
        public decimal Price { get; init; }
        public decimal CleaningFee { get; init; }
        public Guid OwnerId { get; init; }
        public List<Amenity> Amenities { get; init; } = [];
    }
}
