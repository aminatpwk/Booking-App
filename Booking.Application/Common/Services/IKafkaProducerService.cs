using Booking.Application.Common.DTOs;

namespace Booking.Application.Common.Services
{
    public interface IKafkaProducerService
    {
        Task SendErrorLogAsync(ErrorLogDto errorLog);
    }
}
