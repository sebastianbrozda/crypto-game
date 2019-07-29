using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CoinsService.Domain
{
    public interface IDataProvider
    {
        Task<List<Coin>> GetAllCoins();
        Task<List<Quote>> GetAllQuotes();
    }
}
