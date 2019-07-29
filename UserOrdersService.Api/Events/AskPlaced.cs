using System;
using System.Collections.Generic;
using System.Text;
using UserOrdersService.Api.Commands;

namespace UserOrdersService.Api.Events
{
    public class AskPlaced : EventBase
    {
        public static readonly string EVENT_NAME = "AskPlaced";

        public Guid UserId { get; set; }
        public string CoinId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public override string EventName { get; set; }
        public DateTime CreatedAt { get; set; }

        public AskPlaced() : base(EVENT_NAME) { }

        public static AskPlaced CreateFrom(CreateAskCommand cmd)
        {
            return new AskPlaced(cmd.UserId, cmd.CoinId, cmd.Price, cmd.Quantity);
        }

        public AskPlaced(Guid userId, string coinId, decimal price, int quantity) : this()
        {
            this.UserId = userId;
            this.CoinId = coinId;
            this.Price = price;
            this.Quantity = quantity;
            this.CreatedAt = DateTime.UtcNow;
        }
    }
}
