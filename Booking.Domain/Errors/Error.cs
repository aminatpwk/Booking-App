using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Errors
{
    public record Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        private Error(string code, string message, ErrorType type)
        {
            Code = code;
            Message = message;
            Type = type;
        }


        public static Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static Error NullValue = new("Error.NullValue", "Value cannot be null.", ErrorType.Failure);
        public static Error NotFound(string code, string message) => new(code, message, ErrorType.NotFound);
        public static Error GeneralError = new("Error.Failure", "Something went wrong.", ErrorType.Failure);
        public static Error RelationError = new("Error.Relation", "This record has a cascade deleting error.", ErrorType.Conflict);
        public static Error Conflict(string code, string message) => new(code, message, ErrorType.Conflict);
        public static Error BadRequest(string code, string message) => new(code, message, ErrorType.BadRequest);
        public static Error Unauthorized(string code, string message) => new(code, message, ErrorType.Unauthorized);
        public static Error Failure(string code, string message) => new(code, message, ErrorType.Failure);
        public static Error Forbidden(string code, string message) => new(code, message, ErrorType.Forbidden);

        public static Error EmailSendError(string message, string? recipient = null)
        {
            var fullMessage = recipient != null ? $"Failed to send email to {recipient}: {message}" : $"Failed to send email: {message}";
            return new("Email.SendFailed", fullMessage, ErrorType.Failure);
        }
    }
}
