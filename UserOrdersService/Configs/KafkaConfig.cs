using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserOrdersService.Configs
{
    public class KafkaConfig
    {
        public string BootstrapServers { get; set; }
        public string PlacedOrdersTopic { get; set; }
    }
}
