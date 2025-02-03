using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.Common.Repositories;
using Serilog;

namespace Orderflow.Features.Instruments.ListInstruments.Services;

public class ListInstrumentsService : IListInstrumentsService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IInstrumentRepository _repository;

    public ListInstrumentsService(
        IInstrumentRepository repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> ListInstruments()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var instruments = result.AsT0;

        return result;
    }
}