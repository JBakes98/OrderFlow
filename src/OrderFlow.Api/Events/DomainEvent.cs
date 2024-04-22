namespace OrderFlow.Events;

public abstract class DomainEvent
{
    protected DomainEvent(DateTime createdOn) => this.CreatedOn = createdOn;
    public DateTime CreatedOn { get; private set; }
    public abstract string StreamId { get; }
    public string StreamName { get; private set; }
    public Guid? EventId { get; private set; }
}