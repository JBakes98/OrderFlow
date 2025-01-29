namespace Orderflow.Features.Orders.Common.Interfaces;

public interface IOrderBookManager
{
    public IOrderBook GetOrderBook(Guid instrumentId);
}