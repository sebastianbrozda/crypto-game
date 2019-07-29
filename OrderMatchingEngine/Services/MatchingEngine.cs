using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OrderMatchingEngine.RestClients;
using Topshelf;
using System.Threading;
using CoinsService.Api.Queries.Dtos;
using OrderMatchingEngine.DataAccess;
using OrderMatchingEngine.Domain;
using OrderMatchingEngine.Messaging;

namespace OrderMatchingEngine.Services
{
    class MatchingEngine : IMatchingEngine
    {
        private readonly IPriceRestClient _priceRestClient;
        private readonly IDbAccess _dbAccess;
        private readonly IPublisherMessageBroker _publisherMessageBroker;
        private readonly CancellationTokenSource _cancel;

        public MatchingEngine(IPriceRestClient priceRestClient, IDbAccess dbAccess, IPublisherMessageBroker publisherMessageBroker)
        {
            _priceRestClient = priceRestClient;
            _dbAccess = dbAccess;
            _publisherMessageBroker = publisherMessageBroker;
            _cancel = new CancellationTokenSource();
        }

        public void Start()
        {
            Console.WriteLine("MatchingEngine: Started the service");

            Task.Run(() => Background(_cancel.Token));
        }

        public void Stop(HostControl hostControl)
        {
            Console.WriteLine("MatchingEngine: Stopped the service");

            _cancel.Cancel();
        }

        async Task Background(CancellationToken cancellationToken)
        {
            while (!cancellationToken.IsCancellationRequested)
            {
                var allQuotesResult = await _priceRestClient.GetAll();

                var openedOrders = await _dbAccess.GetAllOrdersByStatus(OrderStatus.Opened);

                foreach (var openedOrder in openedOrders)
                {
                    var quote = allQuotesResult.Quotes.FirstOrDefault(f => f.Id == openedOrder.CoinId);
                    var price = GetPrice(quote);
                    openedOrder.Match(price);

                    if (openedOrder.IsClosed)
                    {
                        await _publisherMessageBroker.PublishEvent(OrderMatched.FromOrder(openedOrder));
                    }

                    await _dbAccess.Update(openedOrder);
                }

                await Task.Delay(1000, cancellationToken);
            }
        }

        private Price GetPrice(QuoteDto quote)
        {
            if (quote == null)
            {
                return Price.Null;
            }

            return Price.CreateUsd(quote.Price);
        }
    }
}
