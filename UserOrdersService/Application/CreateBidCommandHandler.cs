using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using MediatR;
using UserOrdersService.Api.Commands;
using UserOrdersService.Api.Events;
using UserOrdersService.Messaging;

namespace UserOrdersService.Application
{
    public class CreateBidCommandHandler : IRequestHandler<CreateBidCommand>
    {
        private readonly IMessageBroker _messageBroker;

        public CreateBidCommandHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task<Unit> Handle(CreateBidCommand request, CancellationToken cancellationToken)
        {
            await _messageBroker.Publish(BidPlaced.CreateFrom(request));

            return Unit.Value;
        }

        
    }
}
