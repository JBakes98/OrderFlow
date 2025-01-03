using Orderflow.Domain.Models;

namespace Orderflow.Services;

public interface IOrderBookManager
{
    public OrderBook GetOrderBook(string instrumentId);
}