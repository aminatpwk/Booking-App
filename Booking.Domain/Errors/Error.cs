using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Domain.Errors
{
    public abstract record Error
    {
        public string Code { get; }
        public string Message { get; }
        public ErrorType Type { get; }
        public string? Details { get; }
        public Dictionary<string, object>? Metadata { get; }
        public DateTime Timestamp { get; } = DateTime.UtcNow;
        public string? TraceId { get; }

        protected Error(string code, string message, ErrorType type, string? details = null, Dictionary<string, object>? metadata = null, string? traceId = null)
        {
            Code = code;
            Message = message;
            Type = type;
            Details = details;
            Metadata = metadata;
            TraceId = traceId;
        }

        public static Error None => new NoneError();

        private sealed record NoneError() : Error(string.Empty, string.Empty, ErrorType.Failure);
    }

    public sealed record NotFoundError(string Code, string Message, string? Details = null, Dictionary<string, object>? Metadata = null)
        : Error(Code, Message, ErrorType.NotFound, Details, Metadata);

    public sealed record BadRequestError(string Code, string Message, string? Details = null, Dictionary<string, object>? Metadata = null)
        : Error(Code, Message, ErrorType.BadRequest, Details, Metadata);

    public sealed record ConflictError(string Code, string Message, string? Details = null)
        : Error(Code, Message, ErrorType.Conflict, Details);

    public sealed record UnauthorizedError(string Code, string Message, string? Details = null)
        : Error(Code, Message, ErrorType.Unauthorized, Details);

    public sealed record ForbiddenError(string Code, string Message, string? Details = null)
        : Error(Code, Message, ErrorType.Forbidden, Details);

    public sealed record ValidationError(string Message, Dictionary<string, string[]> ValidationErrors)
        : Error("Validation.Failed", Message, ErrorType.BadRequest, "One or more validation errors occurred!", new Dictionary<string, object> { ["validationErrors"] = ValidationErrors });

    public sealed record DatabaseError(string Message, string? Details = null, string? TraceId = null)
        : Error("Database.Error", Message, ErrorType.Failure, Details, null, TraceId);

    public sealed record ExternalServiceError(string ServiceName, string Message, string? Details = null)
        : Error("ExternalService.Error", Message, ErrorType.Failure, Details, new Dictionary<string, object> { ["serviceName"] = ServiceName });

    public sealed record EmailSendError(string Message, string? Recipient = null, string? Details = null)
        : Error("Email.SendFailed",
                Recipient != null ? $"Failed to send email to {Recipient}: {Message}" : $"Failed to send email: {Message}",
                ErrorType.Failure,
                Details,
                Recipient != null ? new Dictionary<string, object> { ["recipient"] = Recipient } : null);

}
