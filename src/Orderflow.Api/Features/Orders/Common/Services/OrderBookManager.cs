using Orderflow.Features.Orders.Common.Interfaces;

namespace Orderflow.Features.Orders.Common.Services;

public class OrderBookManager : IOrderBookManager
{
    private readonly Dictionary<Guid, OrderBook> _orderBooks = new();

    public void AddOrderBook(Guid instrumentId)
    {
        if (_orderBooks.ContainsKey(instrumentId))
            return;

        _orderBooks[instrumentId] = new OrderBook();
    }

    public IOrderBook GetOrderBook(Guid instrumentId)
    {
        if (!_orderBooks.ContainsKey(instrumentId))
            AddOrderBook(instrumentId);

        _orderBooks.TryGetValue(instrumentId, out var orderBook);

        return orderBook!;
    }

    public void RemoveOrderBook(Guid instrumentId)
    {
        _orderBooks.Remove(instrumentId);
    }
}