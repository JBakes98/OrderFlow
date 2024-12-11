using Orderflow.Domain.Models.Enums;

namespace Orderflow.Api.Routes.Order.Models;

public class PostOrderRequest
{
    public PostOrderRequest(
        int quantity,
        Guid instrumentId,
        OrderType type)
    {
        Quantity = quantity;
        InstrumentId = instrumentId;
        Type = type;
    }

    public Guid InstrumentId { get; }
    public int Quantity { get; }
    public OrderType Type { get; }
}