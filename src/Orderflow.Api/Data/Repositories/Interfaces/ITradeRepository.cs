using Orderflow.Domain.Models;
using Orderflow.Events.Trade;

namespace Orderflow.Data.Repositories.Interfaces;

public interface ITradeRepository
{
    Task<Error?> InsertAsync(List<Trade> trades, List<TradeExecutedEvent> events);
}