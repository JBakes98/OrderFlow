using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;
using Orderflow.Features.Instruments.Common.Repositories;
using Orderflow.Features.Instruments.CreateInstrument.Events;
using Serilog;

namespace Orderflow.Features.Instruments.CreateInstrument.Services;

public class CreateInstrumentService : ICreateInstrumentService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<Instrument, InstrumentCreatedEvent> _instrumentToInstrumentCreatedEvent;
    private readonly IInstrumentRepository _repository;

    public CreateInstrumentService(
        IInstrumentRepository repository,
        IDiagnosticContext diagnosticContext,
        IMapper<Instrument, InstrumentCreatedEvent> instrumentToInstrumentCreatedEvent)
    {
        _instrumentToInstrumentCreatedEvent = Guard.Against.Null(instrumentToInstrumentCreatedEvent);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Instrument, Error>> CreateInstrument(Instrument source)
    {
        var @event = _instrumentToInstrumentCreatedEvent.Map(source);

        var error = await _repository.InsertAsync(source, @event);

        if (error != null)
            return error;

        return source;
    }
}