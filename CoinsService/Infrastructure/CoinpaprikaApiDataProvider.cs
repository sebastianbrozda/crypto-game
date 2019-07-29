using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinpaprikaAPI;
using CoinpaprikaAPI.Entity;
using CoinsService.Domain;

namespace CoinsService.Infrastructure
{
    class CoinpaprikaApiDataProvider : IDataProvider
    {
        private readonly Client _client;

        public CoinpaprikaApiDataProvider(Client client)
        {
            _client = client;
        }

        public async Task<List<Coin>> GetAllCoins()
        {
            var r = await _client.GetCoinsAsync();
            return r.Value.Select(ConvertToCoin).ToList();
        }

        public async Task<List<Quote>> GetAllQuotes()
        {
            var r = await _client.GetTickersAsync();
            return r.Value.Select(ConvertToQuote).Where(c => c != null).ToList();
        }

        static Quote ConvertToQuote(TickerWithQuotesInfo info)
        {
            const string usd = "USD";
        
            var q = info.Quotes[usd];

            return new Quote()
            {
                Symbol = info.Symbol,
                Name = info.Name,
                Id = info.Id,
                Price = q.Price,
                PercentChange12H = q.PercentChange12H,
                PercentChange1H = q.PercentChange1H,
                PercentChange24H = q.PercentChange24H
            };
        }
        static Coin ConvertToCoin(CoinInfo info)
        {
            return new Coin()
            {
                Symbol = info.Symbol,
                IsActive = info.IsActive,
                Name = info.Name,
                Type = info.Type,
                Id = info.Id,
                Rank = info.Rank
            };
        }
    }
}
