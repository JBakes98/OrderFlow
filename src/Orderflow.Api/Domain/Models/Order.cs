using Orderflow.Domain.Models.Enums;

namespace Orderflow.Domain.Models;

public class Order
{
    public Order(
        string id,
        int quantity,
        string instrumentId,
        double price,
        DateTime date,
        OrderType type,
        OrderStatus status = OrderStatus.pending)
    {
        Id = id;
        Quantity = quantity;
        InstrumentId = instrumentId;
        Price = price;
        Value = price * quantity;
        Date = date;
        Type = type;
        Status = status;
    }

    public string Id { get; }
    public int Quantity { get; }
    public string InstrumentId { get; }
    public double Price { get; private set; }
    public DateTime Date { get; }
    public OrderType Type { get; internal set; }
    public OrderStatus Status { get; private set; } = OrderStatus.pending;
    public double Value { get; private set; }

    public void SetPrice(double price)
    {
        Price = price;
        SetValue();
    }

    private void SetValue()
    {
        Value = Price * Quantity;
    }

    private readonly List<OrderStatus> _finalStates = new List<OrderStatus>
    {
        OrderStatus.cancelled,
        OrderStatus.complete
    };

    public bool SetStatus(OrderStatus status)
    {
        if (_finalStates.Contains(Status))
            return false;

        Status = status;
        return true;
    }
}