using Orderflow.Common.Models;
using Orderflow.Features.Trades.Common.Models;

namespace Orderflow.Features.Trades.CreateTrade.Services;

public interface IProcessTradeService
{
    public Task<Error?> ProcessTrades(List<Trade> trades);
}