using System;
using System.Collections.Generic;
using System.Text;
using UserOrdersService.Api.Commands;

namespace UserOrdersService.Api.Events
{
    public class BidPlaced : EventBase
    {
        public static readonly string EVENT_NAME = "BidPlaced";

        public Guid UserId { get; set; }
        public string CoinId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public override string EventName { get; set; }
        public DateTime CreatedAt { get; set; }
    
        public BidPlaced() : base(EVENT_NAME) { }

        public static BidPlaced CreateFrom(CreateBidCommand cmd)
        {
            return new BidPlaced(cmd.UserId, cmd.CoinId, cmd.Price, cmd.Quantity);
        }

        public BidPlaced(Guid userId, string coinId, decimal price, int quantity) : this()
        {
            this.UserId = userId;
            this.CoinId = coinId;
            this.Price = price;
            this.Quantity = quantity;
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}
