using Orderflow.Features.Common.Models;
using Orderflow.Features.Trades.Common.Models;
using Orderflow.Features.Trades.CreateTrade.Events;

namespace Orderflow.Features.Trades.Common.Repositories;

public interface ITradeRepository
{
    Task<Error?> InsertAsync(List<Trade> trades, List<TradeExecutedEvent> events);
}