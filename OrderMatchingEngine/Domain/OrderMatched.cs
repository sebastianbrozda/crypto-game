using System;
using System.Collections.Generic;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OrderMatchingEngine.Domain
{
    class OrderMatched : IEvent
    {
        [JsonProperty("order_id")]
        public Guid OrderId { get; set; }

        [JsonProperty("user_id")]
        public Guid UserId { get; set; }

        [JsonProperty("coin_id")]
        public string CoinId { get; set; }

        [JsonProperty("order_price")]
        public Price OrderPrice { get; set; }

        [JsonProperty("closed_price")]
        public Price ClosedPrice { get; set; }

        [JsonProperty("quantity")]
        public int Quantity { get; set; }

        [JsonProperty("order_type")]
        [JsonConverter(typeof(StringEnumConverter))]
        public OrderType OrderType { get; set; }

        [JsonProperty("created_at")]
        public DateTime CreatedAt { get; set; }

        [JsonProperty("event_name")]
        public string EventName { get; set; }

        public static OrderMatched FromOrder(Order order)
        {
            return new OrderMatched()
            {
                OrderId = order.OrderId,
                UserId = order.UserId,
                CoinId = order.CoinId,
                OrderPrice = order.OrderPrice,
                OrderType = order.Type,
                Quantity = order.Quantity,
                ClosedPrice = order.OrderPrice,
                CreatedAt = order.CreatedAt,
                EventName = "ORDER_MATCHED"
            };
        }
    }
}
