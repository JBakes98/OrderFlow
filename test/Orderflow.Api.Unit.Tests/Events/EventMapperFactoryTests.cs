using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Events;

namespace Orderflow.Api.Unit.Tests.Events;

public class EventMapperFactoryTests
{
    [Theory, AutoMoqData]
    public void Should_create_outbox_event_from_instrument_created_event(
        InstrumentCreatedEvent @event,
        EventMapperFactory sut)
    {
        var outboxEvent = sut.MapEvent(@event);

        Assert.NotNull(outboxEvent.Id);
        Assert.NotNull(outboxEvent.Payload);
        Assert.Equal(nameof(InstrumentCreatedEvent), outboxEvent.EventType);
        Assert.Equal(@event.InstrumentId, outboxEvent.StreamId);
    }

    [Theory, AutoMoqData]
    public void Should_create_outbox_event_from_order_raised_event(
        BuyOrderRaised @event,
        EventMapperFactory sut)
    {
        var outboxEvent = sut.MapEvent(@event);

        Assert.NotNull(outboxEvent.Id);
        Assert.NotNull(outboxEvent.Payload);
        Assert.Equal(nameof(BuyOrderRaised), outboxEvent.EventType);
        Assert.Equal(@event.OrderId, outboxEvent.StreamId);
    }
}