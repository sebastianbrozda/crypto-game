using System;
using System.Collections.Generic;
using System.Text;
using OrderMatchingEngine.DataAccess;
using OrderMatchingEngine.Domain;
using UserOrdersService.Api.Events;

namespace OrderMatchingEngine.EventHandlers
{
    interface IAskPlacedHandler
    {
        void Handle(AskPlaced bidPlaced);
    }

    class AskPlacedHandler : IAskPlacedHandler
    {
        private readonly IDbAccess _dbAccess;

        public AskPlacedHandler(IDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public void Handle(AskPlaced askPlaced)
        {
            _dbAccess.Add(Order.CreateOpened(askPlaced));
        }
    }
}
