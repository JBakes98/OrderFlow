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

    public async Task<Error?> ProcessTrades(List<Trade> trades)
    {
        var tradeEvents = trades.Select(_tradeToTradeExecutedEventMapper.Map).ToList();

        var err = await _repository.InsertAsync(trades, tradeEvents);

        if (err != null)
            return err;

        _diagnosticContext.Set("TradeExecuted", true);

        return null;
    }
}