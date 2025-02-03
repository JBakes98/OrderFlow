using Orderflow.Common.Repositories;
using Orderflow.Features.Exchanges.CreateExchange.Events;
using Orderflow.Features.Instruments.CreateInstrument.Events;
using Orderflow.Features.Orders.CreateOrder.Events;
using Orderflow.Features.Orders.UpdateOrder.Events;
using Orderflow.Features.Trades.CreateTrade.Events;

namespace Orderflow.Common.Events.Factories;

public class OutboxEventMapperFactory : IOutboxEventMapperFactory
{
    public OutboxEvent MapEvent<T>(T @event) where T : IEvent
    {
        OutboxEvent? outboxEvent;

        switch (@event)
        {
            case ExchangeCreatedEvent exchangeCreatedEvent:
                outboxEvent = new OutboxEvent(
                    id: Guid.NewGuid().ToString(),
                    streamId: exchangeCreatedEvent.ExchangeId,
                    eventType: nameof(ExchangeCreatedEvent),
                    timestamp: DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(payload: @event);
                return outboxEvent;

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

            case TradeExecutedEvent tradeExecutedEvent:
                outboxEvent = new OutboxEvent(
                    id: Guid.NewGuid().ToString(),
                    streamId: tradeExecutedEvent.TradeId,
                    eventType: nameof(TradeExecutedEvent),
                    timestamp: DateTime.Now.ToUniversalTime());
                outboxEvent.SetPayload(@event);
                return outboxEvent;

            default:
                throw new ArgumentException(message: $"Unsupported event type: {typeof(T).Name}");
        }
    }
}