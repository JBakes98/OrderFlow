using Orderflow.Features.Common.Repositories;

namespace Orderflow.Features.Common.Events.Factories;

public interface IOutboxEventMapperFactory
{
    OutboxEvent MapEvent<T>(T @event) where T : IEvent;
}