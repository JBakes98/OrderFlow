using Orderflow.Domain.Models.Enums;

namespace Orderflow.Domain.Models;

public class Order
{
    public Order(
        string id,
        int initialQuantity,
        string instrumentId,
        double price,
        DateTime date,
        TradeSide tradeSide,
        OrderStatus status = OrderStatus.pending)
    {
        Id = id;
        InitialQuantity = initialQuantity;
        RemainingQuantity = initialQuantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = price * initialQuantity;
        Date = date;
        TradeSide = tradeSide;
        Status = status;
    }

    public string Id { get; }
    public int InitialQuantity { get; }
    public int RemainingQuantity { get; private set; }
    public string InstrumentId { get; }
    public double Price { get; private set; }
    public DateTime Date { get; }
    public TradeSide TradeSide { get; internal set; }
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

    public void Fill(int quantity)
    {
        if (quantity > GetRemainingQuantity())
            throw new Exception($"Order {Id} cannot be filled for more than its remaining quantity");

        RemainingQuantity -= quantity;
    }

    public bool SetStatus(OrderStatus status)
    {
        if (_finalStates.Contains(Status))
            return false;

        Status = status;
        return true;
    }

    private readonly List<OrderStatus> _finalStates = new List<OrderStatus>
    {
        OrderStatus.cancelled,
        OrderStatus.complete
    };
}