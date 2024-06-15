namespace OrderFlow.Events;

public class OrderCreatedEvent : BaseOrderEvent
{
    public int Quantity { get; set; }
    public double Price { get; set; }
    public string InstrumentId { get; set; }
}