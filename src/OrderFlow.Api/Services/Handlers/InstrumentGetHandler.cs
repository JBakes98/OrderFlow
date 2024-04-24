using Amazon.DynamoDBv2.DataModel;
using OneOf;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Services.Handlers;

public class InstrumentGetHandler : IHandler<Guid, Instrument>
{
    private readonly IRepository<Instrument> _repository;

    public InstrumentGetHandler(IRepository<Instrument> repository)
    {
        _repository = repository;
    }

    public async Task<OneOf<Instrument, Error>> HandleAsync(Guid request, CancellationToken cancellationToken)
    {
        var instrument = await _repository.GetByIdAsync(request.ToString());

        return instrument;
    }
}