using System.Net;
using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Models;

namespace OrderFlow.Services.Handlers;

public class OrderGetHandler : IHandler<Guid, Order>
{
    private readonly IOrderService _orderService;

    public OrderGetHandler(
        IOrderService orderService)
    {
        _orderService = Guard.Against.Null(orderService);
    }

    public async Task<OneOf<Order, Error>> HandleAsync(Guid id, CancellationToken cancellationToken)
    {
        var order = await _orderService.RetrieveOrder(id.ToString());

        if (order.IsT1)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        return order;
    }
}