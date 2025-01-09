namespace Orderflow.Services.Interfaces;

public interface IOrderBookManager
{
    public OrderBook GetOrderBook(Guid instrumentId);
}