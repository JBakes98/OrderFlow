using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Contracts.Requests;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Services.Handlers;

public class InstrumentCreateHandler : IHandler<CreateInstrument, Instrument>
{
    private readonly IMapper<CreateInstrument, Instrument> _createInstrumentToInstrumentMapper;
    private readonly IInstrumentService _instrumentService;

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