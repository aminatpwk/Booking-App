using Booking.Domain.Errors;
using System.Text.Json;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Booking.Application.Common.Exceptions
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch(Exception ex)
            {
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.", context.Request.Path, context.Request.Method);
                var error = MapExceptionToError(ex);
                var statusCode = MapErrorTypeToStatusCode(error.Type);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var response = CreateErrorResponse(error, ex);

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

        private Error MapExceptionToError(Exception ex) => ex switch
        {
            FluentValidation.ValidationException fve => new ValidationError("One or more validation errors occurred!", FormatFluentValidationErrors(fve)),
            System.ComponentModel.DataAnnotations.ValidationException ve => new BadRequestError("ValidationError", ve.Message),
            EmailSendException ese => new EmailSendError("Email.SendFailed", ese.Message),
            ArgumentNullException => Error.None, 
            KeyNotFoundException => new NotFoundError("KeyNotFound", ex.Message),
            UnauthorizedAccessException => new UnauthorizedError("Error.Unauthorized", "Access denied."),
            _ => new BadRequestError("Error.General", "Something went wrong.") 
        };

        private int MapErrorTypeToStatusCode(ErrorType type) => type switch
        {
            ErrorType.BadRequest => (int)HttpStatusCode.BadRequest,
            ErrorType.NotFound => (int)HttpStatusCode.NotFound,
            ErrorType.Unauthorized => (int)HttpStatusCode.Unauthorized,
            ErrorType.Forbidden => (int)HttpStatusCode.Forbidden,
            ErrorType.Conflict => (int)HttpStatusCode.Conflict,
            ErrorType.Validation => (int)HttpStatusCode.BadRequest,
            ErrorType.ExternalService => (int)HttpStatusCode.BadGateway,
            _=> (int)HttpStatusCode.InternalServerError
        };

        private Dictionary<string, string[]> FormatFluentValidationErrors(FluentValidation.ValidationException exception)
        {
            return exception.Errors
                .GroupBy(e => e.PropertyName)
                .ToDictionary(
                    g => g.Key,
                    g => g.Select(e => e.ErrorMessage).ToArray()
                );
        }

        private object CreateErrorResponse(Error error, Exception originalException)
        {
            var response = new Dictionary<string, object>
            {
                ["code"] = error.Code,
                ["message"] = error.Message,
                ["type"] = error.Type.ToString(),
                ["timestamp"] = error.Timestamp.ToString("yyyy-MM-ddTHH:mm:ss.fffZ")
            };

            if (!string.IsNullOrEmpty(error.Details))
            {
                response["details"] = error.Details;
            }

            if(error.Metadata != null && error.Metadata.Any())
            {
                response["metadata"] = error.Metadata;
            }

            return response;
        }
    }
}
