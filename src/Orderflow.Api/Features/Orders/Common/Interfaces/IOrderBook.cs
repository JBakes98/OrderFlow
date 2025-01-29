using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Trades.Common.Models;

namespace Orderflow.Features.Orders.Common.Interfaces;

public interface IOrderBook
{
    public List<Trade> AddOrder(Order order);
    public void CancelOrders(List<Guid> orderIds);
    public (List<Order>, List<Order>) GetOrderBook();
}