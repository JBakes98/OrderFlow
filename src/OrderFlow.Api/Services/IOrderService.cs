using OneOf;
using OrderFlow.Models;

namespace OrderFlow.Services;

public interface IOrderService
{
    Task<OneOf<Order, Error>> RetrieveOrder(string id);
    Task<OneOf<IEnumerable<Order>, Error>> RetrieveOrders();
}