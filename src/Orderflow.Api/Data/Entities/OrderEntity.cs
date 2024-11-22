using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Orderflow.Data.Entities;

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
        double value,
        DateTime orderDate)
    {
        Id = id;
        Quantity = quantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = value;
        Date = orderDate;
    }

    [Key] [MaxLength(36)] public string Id { get; private set; }
    public int Quantity { get; private set; }
    public double Price { get; private set; }
    public double Value { get; private set; }
    public DateTime Date { get; private set; }

    [MaxLength(36)]
    [ForeignKey("Instrument")]
    public string InstrumentId { get; private set; }

    public virtual InstrumentEntity Instrument { get; set; }
}