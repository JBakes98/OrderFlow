using System.ComponentModel.DataAnnotations;

namespace OrderFlow.Data.Entities;

public class OrderEntity
{
    public OrderEntity()
    {
    }

    public OrderEntity(
        string id,
        int quantity,
        string instrumentId,
        double price,
        DateTime orderDate)
    {
        Id = id;
        Quantity = quantity;
        InstrumentId = instrumentId;
        Price = price;
        OrderDate = orderDate;
    }

    [Key] public string Id { get; private set; }
    public int Quantity { get; private set; }
    public string InstrumentId { get; private set; }
    public double Price { get; private set; }
    public DateTime OrderDate { get; private set; }
}