namespace OrderFlow.Domain.Models;

public class Order
{
    public Order(string instrumentId, int quantity)
    {
        InstrumentId = instrumentId;
        Quantity = quantity;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public int Quantity { get; set; }
    public string InstrumentId { get; set; }
    public double Price { get; private set; }
    public DateTime OrderDate { get; set; } = DateTime.Now.ToUniversalTime();
    public double OrderValue { get; set; }

    public void SetPrice(double price)
    {
        Price = price;
        SetValue();
    }

    private void SetValue() => OrderValue = Price * Quantity;
}