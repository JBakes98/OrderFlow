using System.Text.Json.Serialization;
using Orderflow.Contracts.Enums;

namespace Orderflow.Api.Routes.Order.Models;

public class GetOrderResponse
{
    public GetOrderResponse(
        string id,
        int quantity,
        string instrumentId,
        double price,
        DateTime date,
        OrderType type,
        OrderStatus status)
    {
        Id = id;
        Quantity = quantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = price * quantity;
        Date = date;
        Type = type;
        Status = status;
    }

    public string Id { get; }
    public int Quantity { get; }
    public string InstrumentId { get; }
    public double Price { get; }
    public DateTime Date { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderType Type { get; }

    [JsonConverter(typeof(JsonStringEnumConverter))]
    public OrderStatus Status { get; }

    public double Value { get; }
}