namespace Orderflow.Services;

public class OrderBookManager : IOrderBookManager
{
    private readonly Dictionary<string, OrderBook> _orderBooks = new();

    public void AddOrderBook(string instrumentId)
    {
        if (_orderBooks.ContainsKey(instrumentId))
            return;

        _orderBooks[instrumentId] = new OrderBook();
    }

    public OrderBook GetOrderBook(string instrumentId)
    {
        if (!_orderBooks.ContainsKey(instrumentId))
            AddOrderBook(instrumentId);

        _orderBooks.TryGetValue(instrumentId, out var orderBook);

        return orderBook!;
    }

    public void RemoveOrderBook(string instrumentId)
    {
        _orderBooks.Remove(instrumentId);
    }
}