using OneOf;
using OrderFlow.Domain.Models;

namespace OrderFlow.Services.Handlers;

public class OrderGetHandler : IHandler<string, Order>
{
    private readonly IOrderService _orderService;

    public OrderGetHandler(IOrderService orderService)
    {
        _orderService = orderService;
    }

    public async Task<OneOf<Order, Error>> HandleAsync(string request, CancellationToken cancellationToken)
    {
        var order = await _orderService.RetrieveOrder(request);

        return order;
    }
}