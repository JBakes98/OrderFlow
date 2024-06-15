using OneOf;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Services.Handlers;

public class OrderGetHandler : IHandler<Guid, Order>
{
    private readonly IRepository<Order> _repository;

    public OrderGetHandler(IRepository<Order> repository)
    {
        _repository = repository;
    }

    public async Task<OneOf<Order, Error>> HandleAsync(Guid request, CancellationToken cancellationToken)
    {
        var order = await _repository.GetByIdAsync(request.ToString());

        return order;
    }
}