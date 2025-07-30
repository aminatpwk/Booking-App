using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using Booking.Domain.Apartments;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Photos
{
    public class Photo
    {
        [Key]
        public Guid Id { get; set; }
        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        //alternative: mund te ruhet dhe si vektor bytesh dhe pastaj te konvertohet ne base64 per tu cuar ne frontend
        public string ImageBase64 { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
