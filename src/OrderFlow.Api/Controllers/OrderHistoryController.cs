using Ardalis.GuardClauses;
using Microsoft.AspNetCore.Mvc;
using OrderFlow.Services;

namespace OrderFlow.Controllers;

[Route("api/[controller]")]
[ApiController]
public class OrderHistoryController : ControllerBase
{
    private readonly IOrderService _orderService;

    public OrderHistoryController(IOrderService orderService)
    {
        _orderService = Guard.Against.Null(orderService);
    }

    // POST: api/OrderHistory
    [HttpPost]
    public async Task<IActionResult> PostOrderHistory([FromForm] IFormFile file)
    {
        var result = await _orderService.ProcessOrderHistory(file);

        return Accepted(result);
    }
}