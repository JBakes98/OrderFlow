namespace Orderflow.Features.Orders.CreateOrder.Events;

public class SellOrderRaised : OrderRaisedEvent
{
    public SellOrderRaised(string orderId, string instrumentId, int quantity, double price, double value, string status)
        : base(orderId, instrumentId, quantity, price, value, status)
    {
    }
}