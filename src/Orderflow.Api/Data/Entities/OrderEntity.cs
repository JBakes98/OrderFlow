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
        Guid id,
        int initialQuantity,
        int remainingQuantity,
        Guid instrumentId,
        double price,
        double value,
        DateTime placed,
        DateTime updated,
        TradeSide side,
        OrderStatus status)
    {
        Id = id;
        InitialQuantity = initialQuantity;
        RemainingQuantity = remainingQuantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = value;
        Placed = placed;
        Updated = updated;
        Side = side;
        Status = status;
    }

    [Key][MaxLength(36)] public Guid Id { get; private set; }

    public int InitialQuantity { get; private set; }

    public int RemainingQuantity { get; private set; }

    public double Price { get; private set; }

    public double Value { get; private set; }

    public DateTime Placed { get; private set; }

    public DateTime Updated { get; private set; }

    public TradeSide Side { get; private set; }

    public OrderStatus Status { get; private set; }

    [ForeignKey("Instrument")] public Guid InstrumentId { get; private set; }

    public virtual InstrumentEntity Instrument { get; init; } = null!;

    public ICollection<TradeEntity>? BuyTrades { get; set; }
    public ICollection<TradeEntity>? SellTrades { get; set; }
}
