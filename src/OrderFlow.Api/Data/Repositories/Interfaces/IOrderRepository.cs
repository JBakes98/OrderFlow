using OneOf;
using OrderFlow.Domain.Models;

namespace OrderFlow.Data.Repositories.Interfaces;

public interface IOrderRepository : IRepository<Order>
{
    public Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(Guid instrumentId);
}