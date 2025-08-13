using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Model.Email
{
    public class EmailSenderOptions
    {
        public string ApiKey { get; set; }
        public string SenderEmail { get; set; }
        public string SenderName { get; set; }
        public Dictionary<string, EmailTemplate> Templates { get; set; } = new();
    }
}
