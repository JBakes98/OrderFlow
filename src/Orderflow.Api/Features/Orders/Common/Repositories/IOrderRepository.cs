using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.CreateOrder.Events;

namespace Orderflow.Features.Orders.Common.Repositories;

public interface IOrderRepository
{
    Task<OneOf<IEnumerable<Order>, Error>> QueryAsync();
    Task<OneOf<Order, Error>> GetByIdAsync(string id);
    Task<Error?> InsertAsync(Order entity, OrderRaisedEvent @event);
    Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(string instrumentId);
}