using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.Common.Repositories;
using Serilog;

namespace Orderflow.Features.Exchanges.GetExchange.Services;

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