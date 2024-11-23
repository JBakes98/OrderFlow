namespace Orderflow.Domain.Models;

public class Order
{
    public Order(
        string id,
        int quantity,
        string instrumentId,
        double price,
        DateTime orderDate
    )
    {
        Id = id;
        Quantity = quantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = price * quantity;
        Date = orderDate;
    }

    public string Id { get; }
    public int Quantity { get; }
    public string InstrumentId { get; }
    public double Price { get; private set; }
    public DateTime Date { get; }
    public double Value { get; private set; }

    public void SetPrice(double price)
    {
        Price = price;
        SetValue();
    }

    private void SetValue()
    {
        Value = Price * Quantity;
    }
}