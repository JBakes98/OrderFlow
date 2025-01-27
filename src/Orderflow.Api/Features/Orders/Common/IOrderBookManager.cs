namespace Orderflow.Features.Orders.Common;

public interface IOrderBookManager
{
    public IOrderBook GetOrderBook(Guid instrumentId);
}