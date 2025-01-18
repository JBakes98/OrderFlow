using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface ITradeService
{
    public Task<Error?> ProcessTrades(List<Trade> trades);
}