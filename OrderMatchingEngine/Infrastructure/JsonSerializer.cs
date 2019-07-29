using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OrderMatchingEngine.Serializers;

namespace OrderMatchingEngine.Infrastructure
{
    class JsonSerializer : IJsonSerializer
    {
        public string GetPropertyValueByName(string propertyName, string json)
        {
            return JObject.Parse(json)[propertyName].ToString();
        }

        public T Deserialize<T>(string json)
        {
            return JsonConvert.DeserializeObject<T>(json);
        }

        public string Serialize(object obj)
        {
            return JsonConvert.SerializeObject(obj);
        }
    }
}
