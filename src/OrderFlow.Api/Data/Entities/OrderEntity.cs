namespace OrderFlow.Data.Entities;

public class OrderEntity(
    string id,
    int quantity,
    string instrumentId,
    double price,
    DateTime orderDate)
{
    public string Id { get; } = id;
    public int Quantity { get; } = quantity;
    public string InstrumentId { get; } = instrumentId;
    public double Price { get; } = price;
    public DateTime OrderDate { get; } = orderDate;
}