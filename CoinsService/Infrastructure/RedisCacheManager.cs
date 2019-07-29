using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinsService.Application;
using Microsoft.Extensions.Caching.Memory;

namespace CoinsService.Infrastructure
{
    public class RedisCacheManager : IMemoryCache
    {
        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public bool TryGetValue(object key, out object value)
        {
            throw new NotImplementedException();
        }

        public ICacheEntry CreateEntry(object key)
        {
            throw new NotImplementedException();
        }

        public void Remove(object key)
        {
            throw new NotImplementedException();
        }
    }
}
