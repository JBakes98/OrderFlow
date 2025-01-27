using System.ComponentModel.DataAnnotations.Schema;
using Orderflow.Features.Orders.Common;

namespace Orderflow.Features.Trades.Common;

public class TradeEntity
{
    public TradeEntity()
    {
    }

    public TradeEntity(Guid id, Guid buyOrderId, Guid sellOrderId, double price, int quantity, double value,
        DateTime timestamp)
    {
        Id = id;
        BuyOrderId = buyOrderId;
        SellOrderId = sellOrderId;
        Price = price;
        Quantity = quantity;
        Value = value;
        Timestamp = timestamp;
    }

    public Guid Id { get; private set; }
    [ForeignKey("Order")] public Guid BuyOrderId { get; private set; }
    [ForeignKey("Order")] public Guid SellOrderId { get; private set; }
    public double Price { get; private set; }
    public int Quantity { get; private set; }
    public double Value { get; private set; }
    public DateTime Timestamp { get; private set; }

    public virtual OrderEntity BuyOrder { get; init; } = null!;
    public virtual OrderEntity SellOrder { get; init; } = null!;
}