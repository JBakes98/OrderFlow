using Orderflow.Events;

namespace Orderflow.Features.Orders.CreateOrder.Events;

public class OrderRaisedEvent : IEvent
{
    public OrderRaisedEvent(
        string orderId,
        string instrumentId,
        int quantity,
        double price,
        double value,
        string status)
    {
        OrderId = orderId;
        InstrumentId = instrumentId;
        Quantity = quantity;
        Price = price;
        Value = value;
        Status = status;
    }

    public string OrderId { get; }
    public string InstrumentId { get; }
    public int Quantity { get; }
    public double Price { get; }
    public double Value { get; }
    public string Status { get; }
}