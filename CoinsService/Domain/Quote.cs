using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinsService.Api.Queries.Dtos;

namespace CoinsService.Domain
{
    public class Quote
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal PercentChange1H { get; set; }
        public decimal PercentChange12H { get; set; }
        public decimal PercentChange24H { get; set; }

        public QuoteDto ToDto()
        {
            return new QuoteDto()
            {
                Symbol = Symbol,
                Name = Name,
                Id = Id,
                PercentChange24H = PercentChange24H,
                Price = Price,
                PercentChange12H = PercentChange12H,
                PercentChange1H = PercentChange1H
            };
        }
    }
}
