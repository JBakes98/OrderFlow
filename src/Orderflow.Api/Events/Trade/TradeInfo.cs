namespace Orderflow.Events.Trade;

public class TradeInfo
{
    public TradeInfo(string orderId, double price, double quantity)
    {
        OrderId = orderId;
        Price = price;
        Quantity = quantity;
    }

    public string OrderId { get; }
    public double Price { get; }
    public double Quantity { get; }
}