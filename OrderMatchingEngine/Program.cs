using Microsoft.Extensions.Configuration;
using System;
using System.IO;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using OrderMatchingEngine.Configs;
using OrderMatchingEngine.DataAccess;
using OrderMatchingEngine.EventHandlers;
using OrderMatchingEngine.Infrastructure;
using OrderMatchingEngine.Messaging;
using OrderMatchingEngine.RestClients;
using OrderMatchingEngine.Serializers;
using OrderMatchingEngine.Services;
using Topshelf;

namespace OrderMatchingEngine
{
    class Program
    {
        private static IServiceProvider _serviceProvider;

        public static async Task Main(string[] args)
        {
            RegisterServices();

            var matchingEngine = _serviceProvider.GetService<IMatchingEngine>();
            matchingEngine.Start();

            var messageBroker = _serviceProvider.GetService<IConsumerMessageBroker>();
            messageBroker.RunConsumer();

            DisposeServices();

            Console.ReadLine();
        }

        private static void RegisterServices()
        {
            var builder = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .AddEnvironmentVariables();

            var configuration = builder.Build();

            var collection = new ServiceCollection();

            collection.AddSingleton<IConfiguration>(c => configuration);

            collection.AddSingleton<KafkaConfig, KafkaConfig>();
            collection.AddSingleton<MongoConfig, MongoConfig>();

            collection.AddSingleton<IDbAccess, MongoDbAccess>();
            collection.AddSingleton<IJsonSerializer, JsonSerializer>();
            collection.AddSingleton<IBidPlacedHandler, BidPlacedHandler>();
            collection.AddSingleton<IAskPlacedHandler, AskPlacedHandler>();

            collection.AddSingleton<IConsumerMessageBroker, KafkaConsumerMessageBroker>();
            collection.AddSingleton<IPublisherMessageBroker, KafkaPublisherMessageBroker>();

            collection.AddSingleton<IPriceRestClient, PriceRestClient>();
            collection.AddSingleton<IMatchingEngine, MatchingEngine>();

            _serviceProvider = collection.BuildServiceProvider();
        }

        private static void DisposeServices()
        {
            if (_serviceProvider == null)
            {
                return;
            }

            if (_serviceProvider is IDisposable)
            {
                ((IDisposable)_serviceProvider).Dispose();
            }
        }
    }
}
