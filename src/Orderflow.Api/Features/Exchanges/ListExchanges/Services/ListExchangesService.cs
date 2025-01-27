using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Exchanges.Common.Repositories;
using Serilog;

namespace Orderflow.Features.Exchanges.ListExchanges.Services;

public class ListExchangesService : IListExchangesService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IExchangeRepository _repository;

    public ListExchangesService(
        IExchangeRepository repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<IEnumerable<Common.Exchange>, Error>> ListExchanges()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var exchanges = result.AsT0;

        return result;
    }
}