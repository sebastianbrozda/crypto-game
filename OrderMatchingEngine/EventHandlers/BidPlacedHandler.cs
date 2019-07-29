using System;
using System.Collections.Generic;
using System.Text;
using OrderMatchingEngine.DataAccess;
using OrderMatchingEngine.Domain;
using UserOrdersService.Api.Events;

namespace OrderMatchingEngine.EventHandlers
{
    interface IBidPlacedHandler
    {
        void Handle(BidPlaced bidPlaced);
    }

    class BidPlacedHandler : IBidPlacedHandler
    {
        private readonly IDbAccess _dbAccess;

        public BidPlacedHandler(IDbAccess dbAccess)
        {
            _dbAccess = dbAccess;
        }

        public void Handle(BidPlaced bidPlaced)
        {
            _dbAccess.Add(Order.CreateOpened(bidPlaced));
        }
    }
}
