using Ardalis.GuardClauses;
using OrderFlow.Events;
using OrderFlow.Repositories;
using Serilog;

namespace OrderFlow.Services;

public class EnqueueService : IEnqueueService
{
    private readonly IRepository<Event> _repository;
    private readonly IDiagnosticContext _diagnosticContext;

    public EnqueueService(
        IRepository<Event> repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }


    public async Task<bool> PublishEvent(Event @event)
    {
        var result = await _repository.InsertAsync(@event, default);
        _diagnosticContext.Set($"{@event.EventType}:Published", true);
        return result.IsT0;
    }
}