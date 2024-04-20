using OneOf;
using OrderFlow.Models;

namespace OrderFlow.Services.Handlers;

public interface IOrderHandler<in TRequest>
{
    public Task<OneOf<Order, Error>> HandleAsync(TRequest request, CancellationToken cancellationToken);
}