using Booking.Application.Common.Model.Email;
using Booking.Application.Features.Emails;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;

namespace Booking_App.Controllers
{
    [Route("api/v1/[controller]")]
    [ApiController]
    public class EmailController(IEmailService emailService, IEmailTemplateService templateService) : ControllerBase
    {
        [HttpPost("send")]
        public async Task<IActionResult> SendEmail()
        {
            //for testing purposes
            var templateData = new Dictionary<string, object>
            {
                {"ConfirmationUrl", "https://github.com" },
                {"RejectionUrl", "https://linkedin.com" }
            };

            var email = await templateService.CreateEmailFromTemplateAsync("amina.sokoli@fti.edu.al", "BookingConfirmation", templateData);
            await emailService.SendEmailAsync(email);
            return Ok(new { message = "Template email sent!", email.Subject, email.Body });
        }
    }
}
