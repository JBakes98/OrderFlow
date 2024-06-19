using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Extensions;
using OrderFlow.Models;
using OrderFlow.Repositories;
using Instrument = OrderFlow.Models.Instrument;

namespace OrderFlow.Services.Handlers;

public class InstrumentCreateHandler : IHandler<CreateInstrument, Instrument>
{
    private readonly IRepository<Instrument> _instrumentRepository;
    private readonly IMapper<CreateInstrument, Instrument> _createInstrumentToInstrumentMapper;

    public InstrumentCreateHandler(
        IMapper<CreateInstrument, Instrument> createInstrumentToInstrumentMapper,
        IRepository<Instrument> instrumentRepository)
    {
        _instrumentRepository = instrumentRepository;
        _createInstrumentToInstrumentMapper = createInstrumentToInstrumentMapper;
    }

    public async Task<OneOf<Instrument, Error>> HandleAsync(CreateInstrument request, CancellationToken cancellationToken)
    {
        var instrument = _createInstrumentToInstrumentMapper.Map(request);

        await _instrumentRepository.InsertAsync(instrument, cancellationToken);

        return instrument;
    }
}