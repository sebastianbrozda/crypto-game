using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Confluent.Kafka;
using Newtonsoft.Json.Linq;
using OrderMatchingEngine.Configs;
using OrderMatchingEngine.EventHandlers;
using OrderMatchingEngine.Serializers;
using OrderMatchingEngine.Services;
using UserOrdersService.Api.Events;

namespace OrderMatchingEngine.Messaging
{
    class KafkaConsumerMessageBroker : IConsumerMessageBroker
    {
        private readonly ConsumerConfig _consumerConfig;
        private readonly KafkaConfig _config;
        private readonly IJsonSerializer _jsonSerializer;
        private readonly IBidPlacedHandler _bidPlacedHandler;
        private readonly IAskPlacedHandler _askPlacedHandler;

        public KafkaConsumerMessageBroker(KafkaConfig config, IJsonSerializer jsonSerializer, IBidPlacedHandler bidPlacedHandler, IAskPlacedHandler askPlacedHandler)
        {
            _config = config;
            _jsonSerializer = jsonSerializer;
            _bidPlacedHandler = bidPlacedHandler;
            _askPlacedHandler = askPlacedHandler;

            _consumerConfig = new ConsumerConfig
            {
                BootstrapServers = config.BootstrapServers,
                GroupId = config.ConsumerGroupId,
                EnableAutoCommit = false,
                StatisticsIntervalMs = 5000,
                SessionTimeoutMs = 6000,
                AutoOffsetReset = AutoOffsetReset.Latest,
                EnablePartitionEof = false
            };
        }

        public void RunConsumer()
        {
            Task.Run(() => { Background(); });
        }

        private void Background()
        {
            Console.WriteLine($"{GetType().Name}: running");

            using (var consumer = CreateConsumer())
            {
                consumer.Subscribe(_config.PlacedOrdersTopic);
                try
                {
                    while (true)
                    {
                        try
                        {
                            var consumeResult = consumer.Consume();
                            var json = consumeResult.Value;
                            var eventName = GetEventName(json);
                            var @event = GetEvent(eventName, json);


                            Console.WriteLine(
                                $"Received message at {consumeResult.TopicPartitionOffset}: {consumeResult.Value}");

                            try
                            {
                                if (@event != null)
                                {
                                    if (@event is BidPlaced)
                                    {
                                        _bidPlacedHandler.Handle((BidPlaced) @event);
                                    }
                                    else if (@event is AskPlaced)
                                    {
                                        _askPlacedHandler.Handle((AskPlaced)@event);
                                    }
                                }

                                consumer.Commit(consumeResult);
                            }
                            catch (KafkaException e)
                            {
                                Console.WriteLine($"Commit error: {e.Error.Reason}");
                            }
                        }
                        catch (ConsumeException e)
                        {
                            Console.WriteLine($"Consume error: {e.Error.Reason}");
                        }
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Closing consumer.");
                    consumer.Close();
                }
            }
        }

        private IConsumer<Ignore, string> CreateConsumer()
        {
            return new ConsumerBuilder<Ignore, string>(_consumerConfig)

                .SetErrorHandler((_, e) => Console.WriteLine($"Error: {e.Reason}"))
                // .SetStatisticsHandler((_, json) => Console.WriteLine($"Statistics: {json}"))
                .SetPartitionsAssignedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Assigned partitions: [{string.Join(", ", partitions)}]");
                })
                .SetPartitionsRevokedHandler((c, partitions) =>
                {
                    Console.WriteLine($"Revoking assignment: [{string.Join(", ", partitions)}]");
                })
                .Build();
        }

        private object GetEvent(string eventName, string json)
        {
            if (eventName == BidPlaced.EVENT_NAME)
            {
                return _jsonSerializer.Deserialize<BidPlaced>(json);
            }
            if (eventName == AskPlaced.EVENT_NAME)
            {
                return _jsonSerializer.Deserialize<AskPlaced>(json);
            }

            return null;
        }

        private string GetEventName(string json)
        {
            return _jsonSerializer.GetPropertyValueByName("EventName", json);
        }
    }
}
