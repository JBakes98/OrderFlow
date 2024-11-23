using Orderflow.Data.Entities;

namespace Orderflow.Events;

public interface IEventMapperFactory
{
    OutboxEvent MapEvent<T>(T @event) where T : IEvent;
}