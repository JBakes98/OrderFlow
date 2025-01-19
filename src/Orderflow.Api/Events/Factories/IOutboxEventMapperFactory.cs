using Orderflow.Data.Entities;

namespace Orderflow.Events.Factories;

public interface IOutboxEventMapperFactory
{
    OutboxEvent MapEvent<T>(T @event) where T : IEvent;
}