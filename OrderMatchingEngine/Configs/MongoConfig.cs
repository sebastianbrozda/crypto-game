using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;

namespace OrderMatchingEngine.Configs
{
    class MongoConfig
    {
        public MongoConfig(IConfiguration configuration)
        {
            var mongo = configuration.GetSection("MongoConfig");
            ConnectionString = mongo["ConnectionString"];
            Database = mongo["Database"];
            OrdersCollection = mongo["OrdersCollection"];
        }

        public string ConnectionString { get; private set; }
        public string Database { get; private set; }
        public string OrdersCollection { get; private set; }
    }
}
