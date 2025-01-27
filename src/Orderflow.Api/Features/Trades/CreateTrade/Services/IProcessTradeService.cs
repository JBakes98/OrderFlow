using Orderflow.Features.Common;
using Orderflow.Features.Trades.Common;

namespace Orderflow.Features.Trades.CreateTrade.Services;

public interface IProcessTradeService
{
    public Task<Error?> ProcessTrades(List<Trade> trades);
}