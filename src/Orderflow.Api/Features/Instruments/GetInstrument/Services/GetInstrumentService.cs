using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.Common.Repositories;
using Serilog;

namespace Orderflow.Features.Instruments.GetInstrument.Services;

public class GetInstrumentService : IGetInstrumentService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IInstrumentRepository _repository;

    public GetInstrumentService(
        IInstrumentRepository repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Instrument, Error>> GetInstrument(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        var instrument = result.AsT0;
        _diagnosticContext.Set("InstrumentEntity", instrument, true);

        return instrument;
    }
}