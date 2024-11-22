using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Events;

namespace Orderflow.Data.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<OneOf<IEnumerable<Order>, Error>> QueryAsync();
    Task<OneOf<Order, Error>> GetByIdAsync(string id);
    Task<Error?> InsertAsync(Order entity, OrderRaisedEvent @event);
    Task<Error?> UpdateAsync(Order source);

    Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(string instrumentId);
}