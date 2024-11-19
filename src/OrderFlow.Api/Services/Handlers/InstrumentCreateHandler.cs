using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Domain.Models;
using OrderFlow.Extensions;

namespace OrderFlow.Services.Handlers;

public class InstrumentCreateHandler : IHandler<CreateInstrument, Instrument>
{
    private readonly IInstrumentService _instrumentService;
    private readonly IMapper<CreateInstrument, Instrument> _createInstrumentToInstrumentMapper;

    public InstrumentCreateHandler(
        IMapper<CreateInstrument, Instrument> createInstrumentToInstrumentMapper,
        IInstrumentService instrumentService)
    {
        _createInstrumentToInstrumentMapper = Guard.Against.Null(createInstrumentToInstrumentMapper);
        _instrumentService = Guard.Against.Null(instrumentService);
    }

    public async Task<OneOf<Instrument, Error>> HandleAsync(CreateInstrument request,
        CancellationToken cancellationToken)
    {
        var instrument = _createInstrumentToInstrumentMapper.Map(request);

        await _instrumentService.CreateInstrument(instrument);

        return instrument;
    }
}