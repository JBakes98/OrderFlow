using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Contexts;
using OrderFlow.Contracts.Requests;
using OrderFlow.Extensions;
using OrderFlow.Models;
using OrderFlow.Repositories;
using Instrument = OrderFlow.Models.Instrument;

namespace OrderFlow.Services.Handlers;

public class InstrumentCreateHandler : IHandler<CreateInstrument, Instrument>
{
    private readonly IInstrumentRepository _repository;
    private readonly IMapper<CreateInstrument, Instrument> _createInstrumentToInstrumentMapper;

    public InstrumentCreateHandler(
        IMapper<CreateInstrument, Instrument> createInstrumentToInstrumentMapper,
        IInstrumentRepository repository)
    {
        _repository = Guard.Against.Null(repository);
        _createInstrumentToInstrumentMapper = Guard.Against.Null(createInstrumentToInstrumentMapper);
    }

    public async Task<OneOf<Instrument, Error>> HandleAsync(CreateInstrument request, CancellationToken cancellationToken)
    {
        var instrument = _createInstrumentToInstrumentMapper.Map(request);

        var result = await _repository.InsertAsync(instrument, cancellationToken);

        if (result.IsT1)
            return result.AsT1;
        
        return result.AsT0;
    }
}