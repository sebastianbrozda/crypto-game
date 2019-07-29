using System;
using System.Collections.Generic;
using System.Text;
using MongoDB.Bson;
using MongoDB.Bson.Serialization.Attributes;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace OrderMatchingEngine.Domain
{
    public class Price
    {
        [JsonProperty("value")]
        [BsonElement("value")]
        public decimal Value { get; }

        [JsonProperty("currency")]
        [JsonConverter(typeof(StringEnumConverter))]
        [BsonElement("currency")]
        [BsonRepresentation(BsonType.String)]
        public Currency Currency { get; }

        public static readonly Price Null = new Price(-1, Currency.USD);

        public bool IsNull => this == Null;

        public Price(decimal value, Currency currency)
        {
            Value = value;
            Currency = currency;
        }

        public static Price CreateUsd(decimal value) => new Price(value, Currency.USD);
    }
}
