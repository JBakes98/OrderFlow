namespace OrderFlow.Events;

public class BaseOrderEvent : DomainEvent
{
    public override string StreamId => OrderId;
    public string OrderId { get; }
    public string InstrumentId { get; }
    public int Quantity { get; }
    public double Price { get; } 
    
    public BaseOrderEvent(
        string orderId,
        string instrumentId,
        int quantity,
        double price,
        DateTime createdOn
        ) : base(createdOn)
    {
        OrderId = orderId;
        InstrumentId = instrumentId;
        Quantity = quantity;
        Price = price;
    }
}