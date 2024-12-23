using Orderflow.Domain.Models.Enums;

namespace Orderflow.Api.Routes.Order.Models;

public class PostOrderRequest
{
    public PostOrderRequest(
        int quantity,
        string instrumentId,
        OrderType type)
    {
        Quantity = quantity;
        InstrumentId = instrumentId;
        Type = type;
    }

    public string InstrumentId { get; }
    public int Quantity { get; }
    public OrderType Type { get; }
}