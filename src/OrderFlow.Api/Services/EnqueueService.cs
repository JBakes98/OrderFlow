using System.Net;
using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Services;

public class EnqueueService : IEnqueueService
{
    private readonly IEventRepository _repository;

    public EnqueueService(IEventRepository repository)
    {
        _repository = Guard.Against.Null(repository);
    }


    public async Task<Error?> PublishEvent(Event source, CancellationToken cancellationToken)
    {
        var result = await _repository.InsertAsync(source, cancellationToken);

        if (result.IsT1)
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.EventCouldNotBePublished);

        return null;
    }

    public async Task<OneOf<Event, Error>> RetrieveEvent(string id, CancellationToken cancellationToken)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }

    public Task<OneOf<IEnumerable<Event>, Error>> RetrieveStream(string id, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
    }
}