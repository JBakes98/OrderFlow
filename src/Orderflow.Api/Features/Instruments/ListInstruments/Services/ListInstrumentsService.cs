using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events.Instrument;
using Orderflow.Mappers;
using Serilog;

namespace Orderflow.Api.Routes.Instrument.ListInstruments.Services;

public class ListInstrumentsService : IListInstrumentsService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<Domain.Models.Instrument, InstrumentCreatedEvent> _instrumentToInstrumentCreatedEvent;
    private readonly IInstrumentRepository _repository;

    public ListInstrumentsService(
        IInstrumentRepository repository,
        IDiagnosticContext diagnosticContext,
        IMapper<Domain.Models.Instrument, InstrumentCreatedEvent> instrumentToInstrumentCreatedEvent)
    {
        _instrumentToInstrumentCreatedEvent = Guard.Against.Null(instrumentToInstrumentCreatedEvent);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<IEnumerable<Domain.Models.Instrument>, Error>> ListInstruments()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var instruments = result.AsT0;

        return result;
    }
}