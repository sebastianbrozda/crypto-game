using System;
using System.Collections.Generic;
using System.Text;

namespace CoinsService.Api.Queries.Dtos
{
    public class CoinDto
    {
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public long Rank { get; set; }
    }
}
