using System.Text.Json.Serialization;

namespace Orderflow.Domain.Models.Enums;

[JsonConverter(typeof(JsonStringEnumConverter<OrderStatus>))]
public enum OrderType
{
    buy,
    sell
}