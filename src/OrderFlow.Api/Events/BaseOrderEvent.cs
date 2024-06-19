namespace OrderFlow.Events;

public class BaseOrderEvent
{
    public string EventId { get; } = Guid.NewGuid().ToString();
    public DateTime CreatedOn { get; set; }
    public string EventType { get; set; }
    public string OrderId { get; set; }
}