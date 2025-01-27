using Orderflow.Features.Common;
using Orderflow.Features.Trades.CreateTrade.Events;

namespace Orderflow.Features.Trades.Common.Repositories;

public interface ITradeRepository
{
    Task<Error?> InsertAsync(List<Trade> trades, List<TradeExecutedEvent> events);
}