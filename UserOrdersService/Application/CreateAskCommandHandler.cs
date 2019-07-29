
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
    public class CreateAskCommandHandler : IRequestHandler<CreateAskCommand>
    {
        private readonly IMessageBroker _messageBroker;

        public CreateAskCommandHandler(IMessageBroker messageBroker)
        {
            _messageBroker = messageBroker;
        }

        public async Task<Unit> Handle(CreateAskCommand request, CancellationToken cancellationToken)
        {
            await _messageBroker.Publish(AskPlaced.CreateFrom(request));

            return Unit.Value;
        }

        
    }
}
