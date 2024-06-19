using OneOf;
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Services;

public interface IEnqueueService
{
    Task<Error?> PublishEvent(Event source, CancellationToken cancellationToken);
    Task<OneOf<Event, Error>> RetrieveEvent(string id, CancellationToken cancellationToken);
    Task<OneOf<IEnumerable<Event>, Error>> RetrieveStream(string id, CancellationToken cancellationToken);
}