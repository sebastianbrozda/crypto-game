using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace OrderMatchingEngine.Configs
{
  
    public class KafkaConfig
    {
        public string BootstrapServers { get; }
        public string PlacedOrdersTopic { get; }
        public string ConsumerGroupId { get; }
        public string MatchedOrdersTopic { get; }

        public KafkaConfig(IConfiguration configuration)
        {
            var kafka = configuration.GetSection("KafkaConfig");

            BootstrapServers = kafka["BootstrapServers"];
            PlacedOrdersTopic = kafka["PlacedOrdersTopic"];
            MatchedOrdersTopic = kafka["MatchedOrdersTopic"];
            ConsumerGroupId = kafka["ConsumerGroupId"];
        }
    }
}
