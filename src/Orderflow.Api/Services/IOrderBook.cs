using Orderflow.Domain.Models;

namespace Orderflow.Services;

public interface IOrderBook
{
    public List<Trade> AddOrder(Order order);
}