using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Data.Repositories.Interfaces;
using OrderFlow.Domain.Models;
using OrderFlow.Events;
using OrderFlow.Extensions;
using Serilog;

namespace OrderFlow.Services;

public class InstrumentService : IInstrumentService
{
    private readonly IInstrumentRepository _repository;
    private readonly IMapper<Instrument, InstrumentCreatedEvent> _instrumentToInstrumentCreatedEvent;
    private readonly IDiagnosticContext _diagnosticContext;

    public InstrumentService(
        IInstrumentRepository repository,
        IDiagnosticContext diagnosticContext,
        IMapper<Instrument, InstrumentCreatedEvent> instrumentToInstrumentCreatedEvent)
    {
        _instrumentToInstrumentCreatedEvent = Guard.Against.Null(instrumentToInstrumentCreatedEvent);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Instrument, Error>> RetrieveInstrument(string id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        var instrument = result.AsT0;

        _diagnosticContext.Set($"InstrumentEntity", instrument.Ticker);

        return instrument;
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> RetrieveInstruments()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var instruments = result.AsT0;

        return result;
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