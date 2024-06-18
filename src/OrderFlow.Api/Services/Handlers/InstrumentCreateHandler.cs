using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Contexts;
using OrderFlow.Contracts.Requests;
using OrderFlow.Extensions;
using OrderFlow.Models;
using Instrument = OrderFlow.Models.Instrument;

namespace OrderFlow.Services.Handlers;

public class InstrumentCreateHandler : IHandler<CreateInstrument, Instrument>
{
    private readonly AppDbContext _context;
    private readonly IMapper<CreateInstrument, Instrument> _createInstrumentToInstrumentMapper;

    public InstrumentCreateHandler(
        IMapper<CreateInstrument, Instrument> createInstrumentToInstrumentMapper,
        AppDbContext context)
    {
        _context = Guard.Against.Null(context);
        _createInstrumentToInstrumentMapper = Guard.Against.Null(createInstrumentToInstrumentMapper);
    }

    public async Task<OneOf<Instrument, Error>> HandleAsync(CreateInstrument request, CancellationToken cancellationToken)
    {
        var instrument = _createInstrumentToInstrumentMapper.Map(request);

        _context.Instruments.Add(instrument);
        await _context.SaveChangesAsync(cancellationToken);
        return instrument;
    }
}