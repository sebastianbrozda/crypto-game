using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserOrdersService.Services
{
    public interface IJsonSerializer
    {
        string Serialize(object obj);
    }
}
