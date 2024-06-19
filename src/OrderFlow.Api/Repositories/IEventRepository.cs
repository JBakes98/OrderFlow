using OneOf;
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public interface IEventRepository
{
    Task<OneOf<IEnumerable<Event>, Error>> QueryAsync(string streamId);
    Task<OneOf<Event, Error>> GetByIdAsync(string id);
    Task<OneOf<Event, Error>> InsertAsync(Event source, CancellationToken cancellationToken);
}