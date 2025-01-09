using Orderflow.Domain.Models.Enums;

namespace Orderflow.Api.Routes.Order.Models;

public class GetOrderResponse
{
    public GetOrderResponse(
        string id,
        int quantity,
        string instrumentId,
        double price,
        double value,
        DateTime placed,
        DateTime updated,
        TradeSide type,
        OrderStatus status)
    {
        Id = id;
        Quantity = quantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = value;
        Placed = placed;
        Updated = updated;
        Type = type;
        Status = status;
    }

    public string Id { get; }
    public int Quantity { get; }
    public string InstrumentId { get; }
    public double Price { get; }
    public DateTime Placed { get; }
    public DateTime Updated { get; }
    public TradeSide Type { get; }
    public OrderStatus Status { get; }
    public double Value { get; }
}