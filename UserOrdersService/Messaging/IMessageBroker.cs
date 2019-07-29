using System;
using System.Threading.Tasks;

namespace UserOrdersService.Messaging
{
    public interface IMessageBroker : IDisposable
    {
        Task Publish(object msg);
    }
}
