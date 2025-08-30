using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.DTOs
{
    public class ErrorLogDto
    {
        public string ErrorMessage { get; set; }
        public string StackTrace { get; set; }
        public DateTime Timestamp { get; set; }
        public string SourceService { get; set; }
        public string ExceptionType { get; set; }
    }
}
