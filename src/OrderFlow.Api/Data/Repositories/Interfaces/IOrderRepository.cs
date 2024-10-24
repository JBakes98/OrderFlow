using OneOf;
using OrderFlow.Data.Entities;
using OrderFlow.Domain.Models;

namespace OrderFlow.Data.Repositories.Interfaces;

public interface IOrderRepository : IRepository<OrderEntity>
{
    public Task<OneOf<IEnumerable<OrderEntity>, Error>> GetInstrumentOrders(Guid instrumentId);
}