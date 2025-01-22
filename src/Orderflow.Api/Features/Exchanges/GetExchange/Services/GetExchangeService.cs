using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Features.Exchanges.Common;
using Orderflow.Features.Exchanges.GetExchange.Services;
using Serilog;

namespace Orderflow.Services;

public class GetExchangeService : IGetExchangeService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IExchangeRepository _repository;

    public GetExchangeService(
        IExchangeRepository repository,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Exchange, Error>> GetExchangeById(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        var exchange = result.AsT0;
        _diagnosticContext.Set("ExchangeEntity", exchange, true);

        return exchange;
    }
}