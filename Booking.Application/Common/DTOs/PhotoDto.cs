using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs
{
    public class PhotoDto
    {
        public Guid Id { get; set; }
        public string ImageName { get; set; }
        public string ImageType { get; set; }
        public string Base64Image { get; set; }
    }
}
