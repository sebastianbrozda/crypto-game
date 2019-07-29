using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Microsoft.Extensions.Configuration;
using OrderMatchingEngine.Configs;
using OrderMatchingEngine.Domain;
using OrderMatchingEngine.Serializers;

namespace OrderMatchingEngine.Messaging
{
    class KafkaPublisherMessageBroker : IPublisherMessageBroker
    {
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IProducer<Null, string> _producer;
        private readonly string _topic;

        public KafkaPublisherMessageBroker(KafkaConfig config, IJsonSerializer jsonSerializer)
        {
            _jsonSerializer = jsonSerializer;
            _producer = new ProducerBuilder<Null, string>(new ProducerConfig()
            {
                BootstrapServers = config.BootstrapServers
            }).Build();

            _topic = config.MatchedOrdersTopic;
        }

        public async Task PublishEvent(IEvent @event)
        {
            await _producer.ProduceAsync(_topic, new Message<Null, string>()
            {
                Value = _jsonSerializer.Serialize(@event),
                Key = null
            });
        }

        public void Dispose()
        {
            _producer?.Dispose();
        }
    }
}
