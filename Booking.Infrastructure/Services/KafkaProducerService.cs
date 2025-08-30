using Booking.Application.Common.DTOs;
using Booking.Application.Common.Services;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace Booking.Infrastructure.Services
{
    public class KafkaProducerService : IKafkaProducerService
    {
        private readonly IProducer<Null, string> _producer;
        private readonly IConfiguration _config;
        private const string TopicName = "error-logs-topic";

        public KafkaProducerService(IConfiguration config)
        {
            _config = config;
            var producerConfig = new ProducerConfig { BootstrapServers = _config["Kafka:BootstrapServers"] };
            _producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        }

        public async Task SendErrorLogAsync(ErrorLogDto errorLog)
        {
            var message = JsonSerializer.Serialize(errorLog);
            await _producer.ProduceAsync(TopicName, new Message<Null, string> { Value = message });
        }
    }
}
