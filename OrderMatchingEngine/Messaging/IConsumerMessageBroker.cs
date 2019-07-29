using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace OrderMatchingEngine.Messaging
{
    interface IConsumerMessageBroker
    {
        void RunConsumer();
    }
}
