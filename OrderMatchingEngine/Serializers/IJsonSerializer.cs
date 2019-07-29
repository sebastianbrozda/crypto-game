using System;
using System.Collections.Generic;
using System.Text;

namespace OrderMatchingEngine.Serializers
{
    interface IJsonSerializer
    {
        string GetPropertyValueByName(string propertyName, string json);
        T Deserialize<T>(string json);
        string Serialize(object obj);
    }
}
