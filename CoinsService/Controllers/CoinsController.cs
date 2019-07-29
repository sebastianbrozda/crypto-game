using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinsService.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoinsService.Controllers
{
    [Route("v1/coins")]
    [ApiController]
    public class CoinsController : Controller
    {
        private readonly IMediator _mediator;

        public CoinsController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllCoinsQuery()));
        }
    }
}