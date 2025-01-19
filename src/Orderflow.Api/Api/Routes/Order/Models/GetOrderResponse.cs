using Orderflow.Domain.Models.Enums;

namespace Orderflow.Api.Routes.Order.Models;

public class GetOrderResponse
{
    public GetOrderResponse(
        string id,
        int quantity,
        int remainingQuantity,
        string instrumentId,
        double price,
        double value,
        DateTime placed,
        DateTime updated,
        TradeSide side,
        OrderStatus status)
    {
        Id = id;
        Quantity = quantity;
        RemainingQuantity = remainingQuantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = value;
        Placed = placed;
        Updated = updated;
        Side = side;
        Status = status;
    }

    public string Id { get; }
    public int Quantity { get; }
    public int RemainingQuantity { get; }
    public string InstrumentId { get; }
    public double Price { get; }
    public DateTime Placed { get; }
    public DateTime Updated { get; }
    public TradeSide Side { get; }
    public OrderStatus Status { get; }
    public double Value { get; }
}