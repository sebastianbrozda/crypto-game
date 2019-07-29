using System;
using System.IO;
using System.Threading.Tasks;
using Confluent.Kafka;
using UserOrdersService.Configs;
using UserOrdersService.Services;

namespace UserOrdersService.Messaging
{
    public class KafkaMessageBroker : IMessageBroker
    {
        private readonly KafkaConfig _config;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProducer<Null, string> _producer;

        public KafkaMessageBroker(KafkaConfig config, IJsonSerializer jsonSerializer)
        {
            _config = config;
            _jsonSerializer = jsonSerializer;
            _producer = new ProducerBuilder<Null, string>(new ProducerConfig() { BootstrapServers = config.BootstrapServers }).Build();
        }


        public async Task Publish(object msg)
        {
            await _producer.ProduceAsync(_config.PlacedOrdersTopic, new Message<Null, string>()
            {
                Value = _jsonSerializer.Serialize(msg),
                Key = null // there is only one partition
            });

        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
