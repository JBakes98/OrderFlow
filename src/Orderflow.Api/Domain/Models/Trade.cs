namespace Orderflow.Domain.Models;

public class Trade
{
    public Trade(Guid buyOrderId, Guid sellOrderId, double price, int quantity)
    {
        Id = Guid.NewGuid();
        BuyOrderId = buyOrderId;
        SellOrderId = sellOrderId;
        Price = price;
        Quantity = quantity;
        Value = price * quantity;
        Timestamp = DateTime.Now.ToUniversalTime();
    }

    public Guid Id { get; }
    public Guid BuyOrderId { get; }
    public Guid SellOrderId { get; }
    public double Price { get; }
    public int Quantity { get; }
    public double Value { get; }
    public DateTime Timestamp { get; }
}