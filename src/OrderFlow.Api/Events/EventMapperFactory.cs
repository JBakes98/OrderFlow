using OrderFlow.Data.Entities;

namespace OrderFlow.Events;

public abstract class EventMapperFactory : IEventMapperFactory
{
    public OutboxEvent MapEvent<T>(T @event) where T : IEvent
    {
        OutboxEvent? outboxEvent;

        switch (@event)
        {
            case InstrumentCreatedEvent instrumentCreatedEvent:
                outboxEvent = new OutboxEvent(
                    id: Guid.NewGuid().ToString(), instrumentCreatedEvent.InstrumentId, nameof(InstrumentCreatedEvent),
                    DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(@event);
                return outboxEvent;

            case OrderRaisedEvent orderRaisedEvent:
                outboxEvent = new OutboxEvent(
                    id: Guid.NewGuid().ToString(), orderRaisedEvent.OrderId, nameof(OrderRaisedEvent),
                    DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(@event);
                return outboxEvent;

            default:
                throw new ArgumentException($"Unsupported event type: {typeof(T).Name}");
        }
    }
}