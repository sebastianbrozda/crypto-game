using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using OrderMatchingEngine.Domain;

namespace OrderMatchingEngine.Messaging
{
    interface IPublisherMessageBroker : IDisposable
    {
        Task PublishEvent(IEvent @event);
    }
}
