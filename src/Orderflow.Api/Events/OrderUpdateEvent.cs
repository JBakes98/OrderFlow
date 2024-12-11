namespace Orderflow.Events;

public class OrderUpdateEvent : IEvent
{
    public OrderUpdateEvent(string orderId, string status)
    {
        OrderId = orderId;
        Status = status;
    }

    public string Status { get; }
    public string OrderId { get; }
}