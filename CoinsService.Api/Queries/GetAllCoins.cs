using System;
using System.Collections.Generic;
using System.Text;
using CoinsService.Api.Queries.Dtos;
using MediatR;

namespace CoinsService.Api.Queries
{
    public class GetAllCoinsQuery : IRequest<GetAllCoinsResult>
    {
    }

    public class GetAllCoinsResult
    {
        public List<CoinDto> Coins { get; set; } = new List<CoinDto>();
    }
}
