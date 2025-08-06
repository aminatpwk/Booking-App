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
                _logger.LogError(ex, "An unhandled exception occurred while processing the request.");
                var error = MapExceptionToError(ex);
                var statusCode = MapErrorTypeToStatusCode(error.Type);

                context.Response.ContentType = "application/json";
                context.Response.StatusCode = statusCode;

                var response = new
                {
                    error.Code,
                    error.Message,
                    error.Type
                };

                var json = JsonSerializer.Serialize(response);
                await context.Response.WriteAsync(json);
            }
        }

        private Error MapExceptionToError(Exception ex) => ex switch
        {
            ValidationException ve => Error.BadRequest("ValidationError", ve.Message),
            ArgumentNullException => Error.NullValue,
            KeyNotFoundException => Error.NotFound("KeyNotFound", ex.Message),
            UnauthorizedAccessException => Error.Unauthorized("Error.Unauthorized", "Access denied."),
            _ => Error.GeneralError
        };

        private int MapErrorTypeToStatusCode(ErrorType type) => type switch
        {
            ErrorType.BadRequest => (int)HttpStatusCode.BadRequest,
            ErrorType.NotFound => (int)HttpStatusCode.NotFound,
            ErrorType.Unauthorized => (int)HttpStatusCode.Unauthorized,
            ErrorType.Forbidden => (int)HttpStatusCode.Forbidden,
            ErrorType.Conflict => (int)HttpStatusCode.Conflict,
            _=> (int)HttpStatusCode.InternalServerError
        };
    }
}
