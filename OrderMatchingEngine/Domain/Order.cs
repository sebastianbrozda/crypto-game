using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using UserOrdersService.Api.Events;

namespace OrderMatchingEngine.Domain
{
    public enum OrderType
    {
        Bid = 1,
        Ask = 2
    }

    public enum OrderStatus
    {
        Opened = 0,
        Closed = 10,
        Failed = 20
    }

    public class Order
    {
        [BsonId]
        [BsonElement("id")]
        [BsonRepresentation(BsonType.String)]
        public Guid OrderId { get; set; }

        [BsonElement("user_id")]
        [BsonRepresentation(BsonType.String)]
        public Guid UserId { get; set; }

        [BsonElement("coin_id")]
        public string CoinId { get; set; }

        [BsonElement("order_price")]
        public Price OrderPrice { get; set; }

        [BsonElement("close_price")]
        public Price ClosePrice { get; set; }

        [BsonElement("quantity")]
        public int Quantity { get; set; }

        [BsonElement("type")]
        [BsonRepresentation(BsonType.Int32)]
        public OrderType Type { get; set; }

        [BsonElement("status")]
        [BsonRepresentation(BsonType.Int32)]
        public OrderStatus Status { get; set; }

        [BsonElement("created_at")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime CreatedAt { get; set; }

        [BsonElement("result_message")]
        [BsonRepresentation(BsonType.String)]
        public string ResultMessage { get; set; }

        [BsonElement("processed_at")]
        [BsonRepresentation(BsonType.DateTime)]
        public DateTime? ProcessedAt { get; set; }

        public bool IsBid => Type == OrderType.Bid;
        public bool IsAsk => Type == OrderType.Ask;

        public bool IsClosed => Status == OrderStatus.Closed;

        public static Order CreateOpened(BidPlaced bidPlaced)
        {
            return new Order()
            {
                Type = OrderType.Bid,
                OrderPrice = Domain.Price.CreateUsd(bidPlaced.Price),
                UserId = bidPlaced.UserId,
                Quantity = bidPlaced.Quantity,
                CoinId = bidPlaced.CoinId,
                CreatedAt = bidPlaced.CreatedAt,
                Status = OrderStatus.Opened
            };
        }

        public static Order CreateOpened(AskPlaced askPlaced)
        {
            return new Order()
            {
                Type = OrderType.Ask,
                OrderPrice = Domain.Price.CreateUsd(askPlaced.Price),
                UserId = askPlaced.UserId,
                Quantity = askPlaced.Quantity,
                CoinId = askPlaced.CoinId,
                CreatedAt = askPlaced.CreatedAt,
                Status = OrderStatus.Opened
            };
        }

        private void MarkAsFailed(string msg)
        {
            Status = OrderStatus.Failed;
            ResultMessage = msg;
            ProcessedAt = DateTime.UtcNow;
        }

        private void MarkAsClosed(Price currentPrice)
        {
            Status = OrderStatus.Closed;
            ResultMessage = "OK";
            ProcessedAt = DateTime.UtcNow;
            ClosePrice = currentPrice;
        }

        public void Match(Price currentPrice)
        {
            if (currentPrice.IsNull)
            {
                MarkAsFailed("Price cannot be found");
            }
            else if (IsBid && OrderPrice.Value <= currentPrice.Value)
            {
                MarkAsClosed(currentPrice);
            }
            else if (IsAsk && OrderPrice.Value >= currentPrice.Value)
            {
                MarkAsClosed(currentPrice);
            }
        }
    }
}
