using System;
using System.Collections.Generic;
using System.Text;
using MediatR;

namespace UserOrdersService.Api.Commands
{
    public class CreateAskCommand : IRequest
    {
        public Guid UserId { get; set; }
        public string CoinId { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
