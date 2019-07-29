using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CoinsService.Api.Queries;
using RestEase;

namespace OrderMatchingEngine.RestClients
{
    public interface IPriceRestClient
    {
        [Get("v1/quotes")]
        Task<GetAllQuotesResult> GetAll();
    }
}
