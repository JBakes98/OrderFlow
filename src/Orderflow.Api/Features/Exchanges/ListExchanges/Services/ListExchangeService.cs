using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events.Exchange;
using Orderflow.Mappers;
using Serilog;

namespace Orderflow.Features.Exchanges.ListExchanges.Services;

public class ListExchangeService : IListExchangesService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IExchangeRepository _repository;

    public ListExchangeService(
        IExchangeRepository repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<IEnumerable<Features.Exchanges.Common.Exchange>, Error>> ListExchanges()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var exchanges = result.AsT0;

        return result;
    }
}