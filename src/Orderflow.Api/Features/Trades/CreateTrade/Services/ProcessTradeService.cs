using Ardalis.GuardClauses;
using Orderflow.Common.Mappers;
using Orderflow.Common.Models;
using Orderflow.Features.Trades.Common.Models;
using Orderflow.Features.Trades.Common.Repositories;
using Orderflow.Features.Trades.CreateTrade.Events;
using Serilog;

namespace Orderflow.Features.Trades.CreateTrade.Services;

public class ProcessTradeService : IProcessTradeService
{
    private readonly ITradeRepository _repository;
    private readonly IMapper<Trade, TradeExecutedEvent> _tradeToTradeExecutedEventMapper;
    private readonly IDiagnosticContext _diagnosticContext;

    public ProcessTradeService(ITradeRepository repository,
        IMapper<Trade, TradeExecutedEvent> tradeToTradeExecutedEventMapper,
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