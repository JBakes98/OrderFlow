using Orderflow.Common.Repositories;

namespace Orderflow.Common.Events.Factories;

public interface IOutboxEventMapperFactory
{
    OutboxEvent MapEvent<T>(T @event) where T : IEvent;
}