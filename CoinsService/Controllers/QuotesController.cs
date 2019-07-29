using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CoinsService.Api.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CoinsService.Controllers
{
    [Route("v1/quotes")]
    [ApiController]
    public class QuotesController : Controller
    {
        private readonly IMediator _mediator;

        public QuotesController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            return Ok(await _mediator.Send(new GetAllQuotesQuery()));
        }
    }
}