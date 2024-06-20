using Ardalis.GuardClauses;
using OrderFlow.Events;
using OrderFlow.Repositories;

namespace OrderFlow.Services;

public class EnqueueService : IEnqueueService
{
    private readonly IRepository<Event> _repository;

    public EnqueueService(
        IRepository<Event> repository)
    {
        _repository = Guard.Against.Null(repository);
    }


    public async Task<bool> PublishEvent(Event @event)
    {
        var result = await _repository.InsertAsync(@event, default);

        return result.IsT0;
    }
}