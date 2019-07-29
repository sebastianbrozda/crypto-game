using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using UserOrdersService.Api.Commands;
using UserOrdersService.Messaging;

namespace UserOrdersService.Controllers
{
    [ApiController]
    [Route("v1/bids")]
    public class BidsController : Controller
    {
        private readonly IMediator _mediator;
        private readonly IMessageBroker _producer;

        public BidsController(IMediator mediator, IMessageBroker producer)
        {
            _mediator = mediator;
            _producer = producer;
        }

      
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]CreateBidCommand cmd)
        {
            var r = await _mediator.Send(cmd);
            return StatusCode(StatusCodes.Status201Created, r);
        }
    }
}