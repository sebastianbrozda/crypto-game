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
    public class GetAllQuotesHandler : IRequestHandler<GetAllQuotesQuery, GetAllQuotesResult>
    {
        private readonly IDataProvider _dataProvider;
        private readonly IMemoryCache _memoryCache;

        public GetAllQuotesHandler(IDataProvider dataProvider, IMemoryCache memoryCache)
        {
            _dataProvider = dataProvider;
            _memoryCache = memoryCache;
        }

        public async Task<GetAllQuotesResult> Handle(GetAllQuotesQuery request, CancellationToken cancellationToken)
        {
            var quotes = await _memoryCache.GetOrCreateAsync(CacheKeys.GetAllQuotes, entry =>
            {
                entry.SlidingExpiration = TimeSpan.FromSeconds(1);

                return _dataProvider.GetAllQuotes();
            });

            return new GetAllQuotesResult()
            {
                Quotes = quotes.Select(f => f.ToDto()).ToList()
            };
        }
    }
}
