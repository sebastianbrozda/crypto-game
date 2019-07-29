using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace UserOrdersService.Services.Impls
{
    public class JsonSerializer : IJsonSerializer
    {
        public string Serialize(object obj)
        {
            return Newtonsoft.Json.JsonConvert.SerializeObject(obj);
        }
    }
}
