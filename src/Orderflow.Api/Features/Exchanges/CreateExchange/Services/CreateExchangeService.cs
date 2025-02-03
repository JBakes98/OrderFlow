using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Common.Mappers;
using Orderflow.Common.Models;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.Common.Repositories;
using Orderflow.Features.Exchanges.CreateExchange.Events;
using Serilog;

namespace Orderflow.Features.Exchanges.CreateExchange.Services;

public class CreateExchangeService : ICreateExchangeService
{
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<Exchange, ExchangeCreatedEvent> _exchangeToExchangeCreatedEvent;
    private readonly IExchangeRepository _repository;

    public CreateExchangeService(
        IExchangeRepository repository,
        IDiagnosticContext diagnosticContext,
        IMapper<Exchange, ExchangeCreatedEvent> exchangeToExchangeCreatedEvent)
    {
        _exchangeToExchangeCreatedEvent = Guard.Against.Null(exchangeToExchangeCreatedEvent);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Exchange, Error>> CreateExchange(Exchange source)
    {
        var @event = _exchangeToExchangeCreatedEvent.Map(source);

        var error = await _repository.InsertAsync(source, @event);

        if (error != null)
            return error;

        return source;
    }
}