using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Events;

namespace OrderFlow.Api.Unit.Tests.Events;

public class EventMapperFactoryTests
{
    [Theory, AutoMoqData]
    public void Should_create_outbox_event_from_instrument_created_event(
        InstrumentCreatedEvent @event,
        EventMapperFactory sut)
    {
        var outboxEvent = sut.MapEvent(@event);

        Assert.NotNull(outboxEvent.Id);
        Assert.NotNull(outboxEvent.Timestamp);
        Assert.NotNull(outboxEvent.Payload);
        Assert.Equal(outboxEvent.EventType, nameof(InstrumentCreatedEvent));
        Assert.Equal(outboxEvent.StreamId, @event.InstrumentId);
    }

    [Theory, AutoMoqData]
    public void Should_create_outbox_event_from_order_raised_event(
        OrderRaisedEvent @event,
        EventMapperFactory sut)
    {
        var outboxEvent = sut.MapEvent(@event);

        Assert.NotNull(outboxEvent.Id);
        Assert.NotNull(outboxEvent.Timestamp);
        Assert.NotNull(outboxEvent.Payload);
        Assert.Equal(outboxEvent.EventType, nameof(OrderRaisedEvent));
        Assert.Equal(outboxEvent.StreamId, @event.OrderId);
    }
}