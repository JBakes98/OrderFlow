using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

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

    [Key] [MaxLength(36)] public string Id { get; private set; }
    public int Quantity { get; private set; }

    [MaxLength(36)]
    [ForeignKey("Instrument")]
    public string InstrumentId { get; private set; }

    public InstrumentEntity Instrument { get; set; }
    public double Price { get; private set; }
    public DateTime OrderDate { get; private set; }
}