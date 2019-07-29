using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using CoinsService.Api.Queries;
using Microsoft.Extensions.Configuration;
using Polly;

namespace OrderMatchingEngine.RestClients
{
    class PriceRestClient : IPriceRestClient
    {
        private readonly IPriceRestClient _client;

        private static readonly AsyncPolicy RetryPolicy = Policy.Handle<HttpRequestException>()
            .WaitAndRetryForeverAsync(i => TimeSpan.FromSeconds(3));

        public PriceRestClient(IConfiguration configuration)
        {
            //var url = configuration["CoinsServiceUrl"];
            //_client = RestEase.RestClient.For<IPriceRestClient>(new Uri(url));
        }

        public async Task<GetAllQuotesResult> GetAll()
        {
            return await RetryPolicy.ExecuteAsync(async () => await _client.GetAll());
        }
    }
}
