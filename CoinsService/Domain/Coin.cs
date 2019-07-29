using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinsService.Api.Queries.Dtos;

namespace CoinsService.Domain
{
    public class Coin
    {
        public string Id { get; set; }
        public string Symbol { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public bool IsActive { get; set; }
        public long Rank { get; set; }

        public CoinDto ToDto()
        {
            return new CoinDto()
            {
                Symbol = Symbol,
                Name = Name,
                IsActive = IsActive,
                Type = Type,
                Rank = Rank
            };
        }
    }
}
