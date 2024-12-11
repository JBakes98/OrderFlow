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
                    id: Guid.NewGuid().ToString(),
                    streamId: instrumentCreatedEvent.InstrumentId,
                    eventType: nameof(InstrumentCreatedEvent),
                    timestamp: DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(payload: @event);
                return outboxEvent;

            case BuyOrderRaised orderRaisedEvent:
                outboxEvent = new OutboxEvent(
                    id: Guid.NewGuid().ToString(),
                    streamId: orderRaisedEvent.OrderId,
                    eventType: nameof(BuyOrderRaised),
                    timestamp: DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(payload: @event);
                return outboxEvent;

            case SellOrderRaised orderRaisedEvent:
                outboxEvent = new OutboxEvent(
                    id: Guid.NewGuid().ToString(),
                    streamId: orderRaisedEvent.OrderId,
                    eventType: nameof(SellOrderRaised),
                    timestamp: DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(payload: @event);
                return outboxEvent;

            case OrderUpdateEvent orderUpdateEvent:
                outboxEvent = new OutboxEvent(
                    id: Guid.NewGuid().ToString(),
                    streamId: orderUpdateEvent.OrderId,
                    eventType: nameof(OrderUpdateEvent),
                    timestamp: DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(@event);
                return outboxEvent;

            default:
                throw new ArgumentException(message: $"Unsupported event type: {typeof(T).Name}");
        }
    }
}