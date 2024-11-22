using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using OneOf;
using Orderflow.Contracts.Requests;
using Orderflow.Domain.Models;
using Orderflow.Services;
using Orderflow.Services.Handlers;

namespace Orderflow.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderController : ControllerBase
{
    private readonly IHandler<CreateOrder, Order> _createHandler;
    private readonly IOrderService _orderService;

    public OrderController(
        IHandler<CreateOrder, Order> createHandler,
        IOrderService orderService)
    {
        _createHandler = Guard.Against.Null(createHandler);
        _orderService = Guard.Against.Null(orderService);
    }

    // GET: api/Order
    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> GetOrder()
    {
        var results = await _orderService.RetrieveOrders();

        return QueryOrdersResponse(results);
    }

    // GET: api/Order/5
    [HttpGet("{id}")]
    // [Authorize]
    public async Task<IActionResult> GetOrder([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _orderService.RetrieveOrder(id.ToString());

        return GetOrderResponse(result);
    }

    // POST: api/Order
    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> PostOrder(CreateOrder request, CancellationToken cancellationToken)
    {
        var result = await _createHandler.HandleAsync(request, cancellationToken);

        return CreateOrderResponse(result);
    }

    private static IActionResult CreateOrderResponse(OneOf<Order, Error> result)
    {
        return result.Match<IActionResult>(
            order => new ObjectResult(order),
            error => new ObjectResult(error));
    }

    private static IActionResult GetOrderResponse(OneOf<Order, Error> result)
    {
        return result.Match<IActionResult>(
            order => new ObjectResult(order),
            error => new ObjectResult(error));
    }

    private static IActionResult QueryOrdersResponse(OneOf<IEnumerable<Order>, Error> result)
    {
        return result.Match<IActionResult>(
            orders => new ObjectResult(orders),
            error => new ObjectResult(error));
    }
}