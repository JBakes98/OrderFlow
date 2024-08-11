using Microsoft.AspNetCore.Mvc;
using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Models;
using OrderFlow.Services;
using OrderFlow.Services.Handlers;
using Instrument = OrderFlow.Models.Instrument;

namespace OrderFlow.Controllers;

[Route("api/[controller]")]
[ApiController]
public class InstrumentController : ControllerBase
{
    private readonly IHandler<CreateInstrument, Instrument> _createHandler;
    private readonly IHandler<string, Instrument> _getInstrumentHandler;
    private readonly IInstrumentService _instrumentService;

    public InstrumentController(
        IHandler<CreateInstrument, Instrument> createHandler,
        IHandler<string, Instrument> getInstrumentHandler,
        IInstrumentService instrumentService
    )
    {
        _createHandler = createHandler;
        _getInstrumentHandler = getInstrumentHandler;
        _instrumentService = instrumentService;
    }

    private const string UserInstrumentsReadScope = "orderflow/read:data";
    private const string UserInstrumentsWriteScope = "orderflow/write:data";

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
    public async Task<IActionResult> GetInstrument([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _getInstrumentHandler.HandleAsync(id.ToString(), cancellationToken);

        return GetInstrumentResponse(result);
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
}