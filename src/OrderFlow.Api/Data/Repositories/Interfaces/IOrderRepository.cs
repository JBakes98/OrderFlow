using OneOf;
using OrderFlow.Domain.Models;
using OrderFlow.Events;

namespace OrderFlow.Data.Repositories.Interfaces;

public interface IOrderRepository
{
    Task<OneOf<IEnumerable<Order>, Error>> QueryAsync();
    Task<OneOf<Order, Error>> GetByIdAsync(string id);
    Task<Error?> InsertAsync(Order entity, OrderRaisedEvent @event);
    Task<Error?> UpdateAsync(Order source);

    Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(string instrumentId);
}