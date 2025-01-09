using OneOf;
using Orderflow.Domain.Commands;
using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface IOrderService
{
    Task<OneOf<Order, Error>> RetrieveOrder(string id);
    Task<OneOf<IEnumerable<Order>, Error>> RetrieveOrders();
    Task<OneOf<IEnumerable<Order>, Error>> RetrieveInstrumentOrders(string instrumentId);
    Task<OneOf<Order, Error>> CreateOrder(Order order);
    Task<Error> ProcessOrderHistory(IFormFile orderFile);
}