using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MongoDB.Driver;
using OrderMatchingEngine.Configs;
using OrderMatchingEngine.Domain;

namespace OrderMatchingEngine.DataAccess
{
    class MongoDbAccess : IDbAccess
    {
        private readonly IMongoCollection<Order> _ordersCollection;

        public MongoDbAccess(MongoConfig mongoConfig)
        {
            var mongoClient = new MongoClient(mongoConfig.ConnectionString);
            var database = mongoClient.GetDatabase(mongoConfig.Database);
            _ordersCollection = database.GetCollection<Order>(mongoConfig.OrdersCollection);
        }

        public async Task<Guid> Add(Order order)
        {
            await _ordersCollection.InsertOneAsync(order);

            return order.OrderId;
        }

        public async Task<List<Order>> GetAllOrdersByStatus(OrderStatus status)
        {
            var filter = Builders<Order>.Filter.Eq(order => order.Status, status);
            var orders = await _ordersCollection.Find(filter).ToListAsync();
            return orders;
        }

        public async Task Update(Order order)
        {
            await _ordersCollection.ReplaceOneAsync(o => o.OrderId == order.OrderId, order);
        }
    }
}
