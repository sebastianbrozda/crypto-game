using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using CoinsService.Api.Queries;
using CoinsService.Domain;
using CoinsService.Infrastructure;
using MediatR;
using Microsoft.Extensions.Caching.Memory;

namespace CoinsService.Application.QueriesHandlers
{
    public class GetAllCoinsHandler : IRequestHandler<GetAllCoinsQuery, GetAllCoinsResult>
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMemoryCache _memoryCache;

        public GetAllCoinsHandler(IDataProvider dataProvider, IMemoryCache memoryCache)
        {
            _dataProvider = dataProvider;
            _memoryCache = memoryCache;
        }

        public async Task<GetAllCoinsResult> Handle(GetAllCoinsQuery request, CancellationToken cancellationToken)
        {
            var coins = await _memoryCache.GetOrCreateAsync(CacheKeys.GetAllCoins, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(10);

                return _dataProvider.GetAllCoins();
            });

            return new GetAllCoinsResult()
            {
                Coins = coins.Select(f => f.ToDto()).ToList()
            };
        }
    }
}
