using Booking.Application.Common.Services;
using Microsoft.AspNetCore.Mvc;
using Booking.Application.Common.DTOs;

namespace Booking_App.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class KafkaTestController : ControllerBase
    {
        private readonly IKafkaProducerService _kafkaProducer;
        public KafkaTestController(IKafkaProducerService kafkaProducer)
        {
            _kafkaProducer = kafkaProducer;
        }

        [HttpPost("send-log-error")]
        public async Task<IActionResult> SendLogError()
        {
            var errorLog = new ErrorLogDto
            {
                ErrorMessage = "Test error message",
                StackTrace = "Test stack trace",
                Timestamp = DateTime.UtcNow,
                SourceService = "BookingApp",
                ExceptionType = "System.Exception"
            };
            await _kafkaProducer.SendErrorLogAsync(errorLog);
            return Ok("Error log sent to Kafka");
        }
    }
}
