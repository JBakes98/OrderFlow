using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Services;

public class OrderService : IOrderService
{
    private readonly IOrderRepository _repository;

    public OrderService(
        IOrderRepository repository)
    {
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Order, Error>> RetrieveOrder(string id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> RetrieveOrders()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var orders = result.AsT0;

        return orders.ToList();
    }
}