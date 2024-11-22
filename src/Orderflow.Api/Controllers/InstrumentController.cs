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
public class InstrumentController : ControllerBase
{
    private readonly IHandler<CreateInstrument, Instrument> _createHandler;
    private readonly IInstrumentService _instrumentService;
    private readonly IOrderService _orderService;

    public InstrumentController(
        IHandler<CreateInstrument, Instrument> createHandler,
        IInstrumentService instrumentService,
        IOrderService orderService)
    {
        _orderService = Guard.Against.Null(orderService);
        _createHandler = Guard.Against.Null(createHandler);
        _instrumentService = Guard.Against.Null(instrumentService);
    }

    // GET: api/<InstrumentController>
    [HttpGet]
    // [Authorize]
    public async Task<IActionResult> GetInstrument()
    {
        var results = await _instrumentService.RetrieveInstruments();

        return QueryInstrumentsResponse(results);
    }

    // GET api/<InstrumentController>/5
    [HttpGet("{id}")]
    // [Authorize]
    public async Task<IActionResult> GetInstrument([FromRoute] Guid id)
    {
        var result = await _instrumentService.RetrieveInstrument(id.ToString());

        return GetInstrumentResponse(result);
    }

    // GET api/Instruments/5/Orders
    [HttpGet("{id}/Orders")]
    // [Authorize]
    public async Task<IActionResult> GetInstrumentOrders([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _orderService.RetrieveInstrumentOrders(id.ToString());

        return QueryOrdersResponse(result);
    }

    // POST api/<InstrumentController>
    [HttpPost]
    // [Authorize]
    public async Task<IActionResult> PostInstrument(CreateInstrument request, CancellationToken cancellationToken)
    {
        var result = await _createHandler.HandleAsync(request, cancellationToken);

        return CreateInstrumentResponse(result);
    }

    private static IActionResult CreateInstrumentResponse(OneOf<Instrument, Error> result)
    {
        return result.Match<IActionResult>(
            instrument => new ObjectResult(instrument),
            error => new ObjectResult(error));
    }

    private static IActionResult GetInstrumentResponse(OneOf<Instrument, Error> result)
    {
        return result.Match<IActionResult>(
            instrument => new ObjectResult(instrument),
            error => new ObjectResult(error));
    }

    private static IActionResult QueryInstrumentsResponse(OneOf<IEnumerable<Instrument>, Error> result)
    {
        return result.Match<IActionResult>(
            instruments => new ObjectResult(instruments),
            error => new ObjectResult(error));
    }

    private static IActionResult QueryOrdersResponse(OneOf<IEnumerable<Order>, Error> result)
    {
        return result.Match<IActionResult>(
            orders => new ObjectResult(orders),
            error => new ObjectResult(error));
    }
}