using Orderflow.Data.Entities;

namespace Orderflow.Events;

public class EventMapperFactory : IEventMapperFactory
{
    public OutboxEvent MapEvent<T>(T @event) where T : IEvent
    {
        OutboxEvent? outboxEvent;

        switch (@event)
        {
            case InstrumentCreatedEvent instrumentCreatedEvent:
                outboxEvent = new OutboxEvent(
                    Guid.NewGuid().ToString(), instrumentCreatedEvent.InstrumentId, nameof(InstrumentCreatedEvent),
                    DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(@event);
                return outboxEvent;

            case BuyOrderRaised orderRaisedEvent:
                outboxEvent = new OutboxEvent(
                    Guid.NewGuid().ToString(), orderRaisedEvent.OrderId, nameof(BuyOrderRaised),
                    DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(@event);
                return outboxEvent;

            case SellOrderRaised orderRaisedEvent:
                outboxEvent = new OutboxEvent(
                    Guid.NewGuid().ToString(), orderRaisedEvent.OrderId, nameof(SellOrderRaised),
                    DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(@event);
                return outboxEvent;

            default:
                throw new ArgumentException($"Unsupported event type: {typeof(T).Name}");
        }
    }
}