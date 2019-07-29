using System;
using System.Collections.Generic;
using System.Text;

namespace UserOrdersService.Api.Events
{
    public abstract class EventBase
    {
        public abstract string EventName { get; set; }

        protected EventBase(string eventName)
        {
            EventName = eventName;
        }
    }
}
