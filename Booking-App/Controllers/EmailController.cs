using Booking.Application.Common.Model;
using Booking.Application.Features.Emails;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmailController(IEmailService emailService) : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail([FromBody] Email email)
        {
            if(string.IsNullOrWhiteSpace(email.To) ||
                string.IsNullOrWhiteSpace(email.Subject) ||
                string.IsNullOrWhiteSpace(email.Body))
            {
                return BadRequest("Email, Subject, and Body cannot be empty.");
            }

            await emailService.SendEmailAsync(email);
            return Ok("Email sent successfully.");
        }
    }
}
