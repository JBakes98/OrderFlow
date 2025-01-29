using Orderflow.Features.Common.Enums;

namespace Orderflow.Features.Orders.Common.Models;

public class Order
{
    public Order(
        int initialQuantity,
        Guid instrumentId,
        double price,
        TradeSide side)
    {
        Id = Guid.NewGuid();
        InitialQuantity = initialQuantity;
        RemainingQuantity = initialQuantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = price * initialQuantity;
        Placed = DateTime.Now.ToUniversalTime();
        Side = side;
        Status = OrderStatus.pending;
    }

    public Order(
        Guid id,
        int initialQuantity,
        int remainingQuantity,
        double value,
        Guid instrumentId,
        double price,
        DateTime placed,
        DateTime updated,
        TradeSide side,
        OrderStatus status)
    {
        Id = id;
        InitialQuantity = initialQuantity;
        RemainingQuantity = remainingQuantity;
        Value = value;
        InstrumentId = instrumentId;
        Price = price;
        Placed = placed;
        Updated = updated;
        Side = side;
        Status = status;
    }

    public Guid Id { get; }
    public int InitialQuantity { get; }
    public int RemainingQuantity { get; private set; }
    public Guid InstrumentId { get; }
    public double Price { get; set; }
    public DateTime Placed { get; }
    public DateTime Updated { get; private set; }
    public TradeSide Side { get; }
    public OrderStatus Status { get; private set; }
    public double Value { get; private set; }

    public void SetPrice(double price)
    {
        Price = price;
        SetValue();
    }

    private void SetValue() => Value = Price * InitialQuantity;
    public int GetRemainingQuantity() => RemainingQuantity;
    public bool IsFilled() => GetRemainingQuantity() == 0;

    public void UpdateQuantity(int volume)
    {
        if (volume > RemainingQuantity)
            throw new Exception($"Order {Id} cannot be filled for more than its remaining quantity");

        if (RemainingQuantity == 0 || _finalStates.Contains(Status))
            throw new Exception($"Order {Id} is in a final state");

        RemainingQuantity -= volume;
        Updated = DateTime.Now;

        UpdateStatus(RemainingQuantity == 0 ? OrderStatus.filled : OrderStatus.part_filled);
    }

    private void UpdateStatus(OrderStatus status)
    {
        if (_finalStates.Contains(Status))
            return;

        Status = status;
    }

    private readonly List<OrderStatus> _finalStates =
    [
        OrderStatus.cancelled,
        OrderStatus.filled
    ];
}