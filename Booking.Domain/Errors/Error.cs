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
        public string? Details { get; }
        public Dictionary<string, object>? Metadata { get; }
        public DateTime Timestamp { get; }
        public string? TraceId {  get; }
        private Error(string code, string message, ErrorType type, string? details = null, Dictionary<string, object>? metadata = null, string? traceId = null)
        {
            Code = code;
            Message = message;
            Type = type;
            Details = details;
            Metadata = metadata;
            Timestamp = DateTime.UtcNow;
            TraceId = traceId;
        }


        public static Error None = new(string.Empty, string.Empty, ErrorType.Failure);
        public static Error NullValue = new("Error.NullValue", "Value cannot be null.", ErrorType.Failure);
        public static Error NotFound(string code, string message, string? details = null, Dictionary<string, object>? metadata = null) => new(code, message, ErrorType.NotFound, details, metadata);
        public static Error GeneralError = new("Error.Failure", "Something went wrong.", ErrorType.Failure);
        public static Error RelationError = new("Error.Relation", "This record has a cascade deleting error.", ErrorType.Conflict);
        public static Error Conflict(string code, string message, string? details = null) => new(code, message, ErrorType.Conflict, details);
        public static Error BadRequest(string code, string message, string? details = null, Dictionary<string, object>? metadata = null) => new(code, message, ErrorType.BadRequest, details, metadata);
        public static Error Unauthorized(string code, string message, string? details = null) => new(code, message, ErrorType.Unauthorized, details);
        public static Error Failure(string code, string message, string? details = null, Dictionary<string, object>? metadata = null, string? traceId = null) => new(code, message, ErrorType.Failure, details, metadata, traceId);
        public static Error Forbidden(string code, string message, string? details) => new(code, message, ErrorType.Forbidden, details);

        public static Error EmailSendError(string message, string? recipient = null, string? details = null)
        {
            var fullMessage = recipient != null ? $"Failed to send email to {recipient}: {message}" : $"Failed to send email: {message}";
            var metadata = recipient != null ? new Dictionary<string, object> { ["recipient"] = recipient } : null;
            return new("Email.SendFailed", fullMessage, ErrorType.Failure, details, metadata);
        }

        public static Error ValidationError(string message, Dictionary<string, string[]> validationErrors)
        {
            var metadata = new Dictionary<string, object> { ["validationErrors"] = validationErrors};
            return new("Validation.Failed", message, ErrorType.BadRequest, "One or more validation errors occurred!", metadata);
        }

        public static Error DatabaseError(string message, string? details = null, string? traceId = null)
        {
            return new("Database.Error", message, ErrorType.Failure, details, null, traceId);
        }

        public static Error ExternalServiceError(string serviceName, string message, string? details = null)
        {
            var metadata = new Dictionary<string, object> { ["serviceName"] = serviceName};
            return new("ExternalService.Error", message, ErrorType.Failure, details, metadata);
        }
    }
}
