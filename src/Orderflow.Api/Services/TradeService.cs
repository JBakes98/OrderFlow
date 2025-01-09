using Ardalis.GuardClauses;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain.Models;
using Orderflow.Events.Trade;
using Orderflow.Mappers;
using Orderflow.Services.Interfaces;
using Serilog;

namespace Orderflow.Services;

public class TradeService : ITradeService
{
    private readonly ITradeRepository _repository;
    private readonly IMapper<Trade, TradeExecutedEvent> _tradeToTradeExecutedEventMapper;
    private readonly IDiagnosticContext _diagnosticContext;

    public TradeService(ITradeRepository repository, IMapper<Trade, TradeExecutedEvent> tradeToTradeExecutedEventMapper,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _tradeToTradeExecutedEventMapper = Guard.Against.Null(tradeToTradeExecutedEventMapper);
        _repository = Guard.Against.Null(repository);
    }

    public async void ProcessTrades(List<Trade> trades)
    {
        foreach (var trade in trades)
        {
            var err = await ProcessTrade(trade);
        }
    }

    private async Task<Error?> ProcessTrade(Trade trade)
    {
        var tradeEvent = _tradeToTradeExecutedEventMapper.Map(trade);

        var err = await _repository.InsertAsync(trade, tradeEvent);

        if (err != null)
            return err;

        _diagnosticContext.Set("Trade", trade, true);
        _diagnosticContext.Set("TradeEvent", tradeEvent, true);
        _diagnosticContext.Set("TradeExecuted", true);

        return null;
    }
}