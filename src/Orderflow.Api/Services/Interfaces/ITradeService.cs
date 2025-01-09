using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface ITradeService
{
    public void ProcessTrades(List<Trade> trades);
}