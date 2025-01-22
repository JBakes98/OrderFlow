using Ardalis.GuardClauses;
using OneOf;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Events.Exchange;
using Orderflow.Mappers;
using Orderflow.Services.Interfaces;
using Serilog;

namespace Orderflow.Services;

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

    public async Task<OneOf<Exchange, Error>> GetExchangeById(Guid id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        var exchange = result.AsT0;
        _diagnosticContext.Set("ExchangeEntity", exchange, true);

        return exchange;
    }

    public async Task<OneOf<IEnumerable<Exchange>, Error>> GetExchanges()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var exchanges = result.AsT0;

        return result;
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