using OrderFlow.Events;

namespace OrderFlow.Services;

public interface IEnqueueService
{
    Task<bool> PublishEvent(Event @event);
}