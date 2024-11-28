using System.Text.Json.Serialization;

namespace Orderflow.Contracts.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<OrderStatus>))]
public enum OrderType
{
    buy,
    sell
}