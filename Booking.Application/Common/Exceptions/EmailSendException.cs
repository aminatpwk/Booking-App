using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Exceptions
{
    public class EmailSendException : Exception
    {
        public string? RecipientEmail { get; }
        public int? StatusCode { get; }

        public EmailSendException(string message) : base(message) { }

        public EmailSendException(string message, string recipientEmail) : base(message) { 
            RecipientEmail = recipientEmail;
        }

        public EmailSendException(string message, string recipientEmail, int statusCode) : base(message)
        {
            RecipientEmail = recipientEmail;
            StatusCode = statusCode;
        }

        public EmailSendException(string message, Exception innerException) : base(message, innerException)
        {

        }
    }
}
