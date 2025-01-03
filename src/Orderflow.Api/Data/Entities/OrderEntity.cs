using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Orderflow.Domain.Models.Enums;

namespace Orderflow.Data.Entities;

public class OrderEntity
{
    public OrderEntity()
    {
    }

    public OrderEntity(
        string id,
        int initialQuantity,
        int remainingQuantity,
        string instrumentId,
        double price,
        double value,
        DateTime date,
        TradeSide type,
        OrderStatus status)
    {
        Id = id;
        InitialQuantity = initialQuantity;
        RemainingQuantity = remainingQuantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = value;
        Date = date;
        Type = type;
        Status = status;
    }

    [Key] [MaxLength(36)] public string Id { get; private set; } = null!;
    public int InitialQuantity { get; private set; }
    public int RemainingQuantity { get; private set; }
    public double Price { get; private set; }
    public double Value { get; private set; }
    public DateTime Date { get; private set; }
    public TradeSide Type { get; private set; }
    public OrderStatus Status { get; private set; }

    [MaxLength(36)]
    [ForeignKey("Instrument")]
    public string InstrumentId { get; private set; } = null!;

    public virtual InstrumentEntity Instrument { get; init; } = null!;

    public void UpdateStatus(OrderStatus status)
    {
        Status = status;
    }
}