namespace Orderflow.Features.Orders.CreateOrder.Events;

public class BuyOrderRaised : OrderRaisedEvent
{
    public BuyOrderRaised(
        string orderId,
        string instrumentId,
        int quantity,
        double price,
        double value,
        string status) : base(
        orderId,
        instrumentId,
        quantity,
        price,
        value,
        status)
    {
    }
}