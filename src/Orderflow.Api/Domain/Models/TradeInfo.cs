namespace Orderflow.Domain.Models;

public class TradeInfo
{
    public TradeInfo(string orderId, double price, double quantity)
    {
        OrderId = orderId;
        Price = price;
        Quantity = quantity;
    }

    public string OrderId { get; set; }
    public double Price { get; set; }
    public double Quantity { get; set; }
}