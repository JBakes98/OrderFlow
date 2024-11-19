using OrderFlow.Data.Entities;

namespace OrderFlow.Events;

public interface IEventMapperFactory
{
    OutboxEvent MapEvent<T>(T @event) where T : IEvent;
}