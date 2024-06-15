using System.Text.Json;
using OrderFlow.Events;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Events;

public class OrderEventToEventMapper : IMapper<BaseOrderEvent, Event>
{
    public Event Map(BaseOrderEvent source)
    {
        return new Event
        {
            @event = JsonSerializer.Serialize(source),
            StreamId = source.OrderId,
            EventType = source.GetType().Name
        };
    }
}