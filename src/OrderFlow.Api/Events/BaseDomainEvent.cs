namespace OrderFlow.Events;

public abstract class BaseDomainEvent
{
    public string EventId { get; }
    public string StreamId { get; }
    public string EventType { get; }
    public DateTime Timestamp { get; }

    protected BaseDomainEvent(string streamId, string eventType)
    {
        EventId = Guid.NewGuid().ToString();
        StreamId = streamId;
        EventType = eventType;
        Timestamp = DateTime.UtcNow;
    }
}