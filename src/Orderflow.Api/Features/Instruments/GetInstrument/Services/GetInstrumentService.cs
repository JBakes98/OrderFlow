using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events.Instrument;
using Orderflow.Mappers;
using Serilog;

namespace Orderflow.Api.Routes.Instrument.GetInstrument.Services;

public class GetInstrumentService : IGetInstrumentService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<Domain.Models.Instrument, InstrumentCreatedEvent> _instrumentToInstrumentCreatedEvent;
    private readonly IInstrumentRepository _repository;

    public GetInstrumentService(
        IInstrumentRepository repository,
        IDiagnosticContext diagnosticContext,
        IMapper<Domain.Models.Instrument, InstrumentCreatedEvent> instrumentToInstrumentCreatedEvent)
    {
        _instrumentToInstrumentCreatedEvent = Guard.Against.Null(instrumentToInstrumentCreatedEvent);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Domain.Models.Instrument, Error>> GetInstrument(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        var instrument = result.AsT0;
        _diagnosticContext.Set("InstrumentEntity", instrument, true);

        return instrument;
    }
}