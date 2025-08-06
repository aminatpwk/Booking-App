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

        [Required]
        public Guid ApartmentId { get; set; }
        public Apartment Apartment { get; set; }

        //ruhet si vektor bytesh pastaj konvertohet ne base64 per tu cuar ne frontend
        public byte[] ImageData { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
