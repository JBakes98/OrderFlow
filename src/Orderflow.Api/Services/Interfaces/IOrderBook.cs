using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface IOrderBook
{
    public List<Trade> AddOrder(Order order);
    public void CancelOrders(List<Guid> orderIds);
}