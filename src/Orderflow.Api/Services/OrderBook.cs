using Orderflow.Domain.Models;
using Orderflow.Domain.Models.Enums;
using Orderflow.Services.Interfaces;

namespace Orderflow.Services;

public class OrderBook : IOrderBook
{
    private readonly SortedDictionary<double, LinkedList<Order>> _bids =
        new(Comparer<double>.Create((x, y) => y.CompareTo(x)));

    private readonly SortedDictionary<double, LinkedList<Order>> _asks = new(Comparer<double>.Default);

    private readonly Dictionary<Guid, Order> _orders = new();

    public void CancelOrders(List<Guid> orderIds)
    {
        foreach (var id in orderIds)
            CancelOrderInternal(id);
    }

    private void CancelOrderInternal(Guid id)
    {
        if (!_orders.ContainsKey(id))
            return;

        _orders.TryGetValue(id, out var orderEntry);

        if (orderEntry == null)
            return;

        var order = orderEntry;
        var price = order.Price;
        LinkedList<Order>? orders;

        if (order.Side == TradeSide.buy)
            _bids.TryGetValue(price, out orders);
        else
            _asks.TryGetValue(price, out orders);

        orders?.Remove(order);
        _orders.Remove(id);
    }

    private bool CanMatch(TradeSide side, double price)
    {
        if (side == TradeSide.buy)
        {
            if (_asks.Count == 0)
                return false;

            var bestAsk = _asks.FirstOrDefault();
            return price >= bestAsk.Key;
        }

        if (_bids.Count == 0)
            return false;

        var bestBid = _bids.FirstOrDefault();
        return price <= bestBid.Key;
    }

    private List<Trade> MatchOrders()
    {
        var trades = new List<Trade>();

        while (true)
        {
            if (_bids.Count == 0 || _asks.Count == 0)
                break;

            var (bidPrice, bids) = _bids.FirstOrDefault();
            var (askPrice, asks) = _asks.FirstOrDefault();

            if (bidPrice < askPrice)
                break;

            while (bids.Count != 0 && asks.Count != 0)
            {
                var bid = bids.FirstOrDefault();
                var ask = asks.FirstOrDefault();

                var quantity = Math.Min(bid.GetRemainingQuantity(), ask.GetRemainingQuantity());

                bid.UpdateQuantity(quantity);
                ask.UpdateQuantity(quantity);

                if (bid.IsFilled())
                {
                    bids.Remove(bid);
                    _orders.Remove(bid.Id);
                }

                if (ask.IsFilled())
                {
                    asks.Remove(ask);
                    _orders.Remove(ask.Id);
                }

                if (_bids.Count == 0)
                    _bids.Remove(bidPrice);

                if (_asks.Count == 0)
                    _asks.Remove(askPrice);

                trades.Add(new Trade(bid.Id, ask.Id, bid.Price, quantity));
            }

            if (bids.Count == 0)
            {
                _bids.Remove(bidPrice);
            }

            if (asks.Count == 0)
            {
                _asks.Remove(askPrice);
            }

            if (_bids.Count != 0)
            {
                (_, bids) = _bids.FirstOrDefault();
                var order = bids.FirstOrDefault();
                CancelOrders([order.Id]);
            }

            if (_asks.Count != 0)
            {
                (_, asks) = _asks.FirstOrDefault();
                var order = asks.FirstOrDefault();
                CancelOrders([order.Id]);
            }
        }

        return trades;
    }

    public List<Trade> AddOrder(Order order)
    {
        if (_orders.ContainsKey(order.Id))
        {
            throw new InvalidOperationException($"Order with ID {order.Id} already exists.");
        }

        _orders[order.Id] = order;
        var targetBook = order.Side == TradeSide.buy ? _bids : _asks;

        if (!targetBook.TryGetValue(order.Price, out var orderList))
        {
            orderList = new LinkedList<Order>();
            targetBook[order.Price] = orderList;
        }

        orderList.AddLast(order);

        return CanMatch(order.Side, order.Price) ? MatchOrders() : [];
    }
}