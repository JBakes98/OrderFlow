using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Events.Order;

namespace Orderflow.Data.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<OneOf<IEnumerable<Order>, Error>> QueryAsync();
    Task<OneOf<Order, Error>> GetByIdAsync(string id);
    Task<Error?> InsertAsync(Order entity, OrderRaisedEvent @event);
    Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(string instrumentId);
}