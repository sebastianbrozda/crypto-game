using System;
using System.Collections.Generic;
using System.Text;

namespace CoinsService.Api.Queries.Dtos
{
    public class QuoteDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Symbol { get; set; }
        public decimal Price { get; set; }
        public decimal PercentChange1H { get; set; }
        public decimal PercentChange12H { get; set; }
        public decimal PercentChange24H { get; set; }
    }
}
