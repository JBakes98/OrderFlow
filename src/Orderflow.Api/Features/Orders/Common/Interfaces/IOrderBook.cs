using Orderflow.Features.Trades.Common;

namespace Orderflow.Features.Orders.Common;

public interface IOrderBook
{
    public List<Trade> AddOrder(Order order);
    public void CancelOrders(List<Guid> orderIds);
    public (List<Order>, List<Order>) GetOrderBook();
}