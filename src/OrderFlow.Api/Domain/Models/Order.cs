namespace OrderFlow.Domain.Models;

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
        OrderDate = orderDate;
    }

    public string Id { get; }
    public int Quantity { get; }
    public string InstrumentId { get; }
    public double Price { get; private set; }
    public DateTime OrderDate { get; }
    public double OrderValue { get; private set; }

    public void SetPrice(double price)
    {
        Price = price;
        SetValue();
    }

    private void SetValue() => OrderValue = Price * Quantity;
}