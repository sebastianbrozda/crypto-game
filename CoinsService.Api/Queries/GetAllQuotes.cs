using System;
using System.Collections.Generic;
using System.Text;
using CoinsService.Api.Queries.Dtos;
using MediatR;

namespace CoinsService.Api.Queries
{
    public class GetAllQuotesQuery : IRequest<GetAllQuotesResult>
    {
    }

    public class GetAllQuotesResult 
    {
        public List<QuoteDto> Quotes { get; set; } = new List<QuoteDto>();
    }
}
