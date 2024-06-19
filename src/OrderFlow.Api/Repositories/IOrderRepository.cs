using OneOf;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public interface IOrderRepository
{
    Task<OneOf<IEnumerable<Order>, Error>> QueryAsync();
    Task<OneOf<Order, Error>> GetByIdAsync(string id);
    Task<OneOf<Order, Error>> InsertAsync(Order source, CancellationToken cancellationToken);
}