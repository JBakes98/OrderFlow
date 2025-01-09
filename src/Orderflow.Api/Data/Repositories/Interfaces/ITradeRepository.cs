using Orderflow.Domain.Models;
using Orderflow.Events.Trade;

namespace Orderflow.Data.Repositories.Interfaces;

public interface ITradeRepository
{
    Task<Error?> InsertAsync(Trade trade, TradeExecutedEvent @event);
}