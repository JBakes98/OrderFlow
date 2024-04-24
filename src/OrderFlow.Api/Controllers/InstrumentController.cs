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
    private readonly IHandler<Guid, Instrument> _getInstrumentHandler;
    private readonly IInstrumentService _instrumentService;

    public InstrumentController(
        IHandler<CreateInstrument, Instrument> createHandler,
        IHandler<Guid, Instrument> getInstrumentHandler,
        IInstrumentService instrumentService
        )
    {
        _createHandler = createHandler;
        _getInstrumentHandler = getInstrumentHandler;
        _instrumentService = instrumentService;
    }

    // GET: api/<InstrumentController>
    [HttpGet]
    public async Task<IActionResult> GetInstrument()
    {
        var results = await _instrumentService.RetrieveInstruments();

        return QueryInstrumentsResponse(results);
    }

    // GET api/<InstrumentController>/5
    [HttpGet("{id}")]
    public async Task<IActionResult> GetInstrument([FromRoute] Guid id, CancellationToken cancellationToken)
    {
        var result = await _getInstrumentHandler.HandleAsync(id, cancellationToken);

        return GetInstrumentResponse(result);
    }

    // POST api/<InstrumentController>
    [HttpPost]
    public async Task<IActionResult> PostInstrument(CreateInstrument request, CancellationToken cancellationToken)
    {
        var result = await _createHandler.HandleAsync(request, cancellationToken);

        return CreateInstrumentResponse(result);
    }

    // // PUT api/<InstrumentController>/5
    // [HttpPut("{id}")]
    // public void Put(int id, [FromBody] string value)
    // {
    // }
    //
    // // DELETE api/<InstrumentController>/5
    // [HttpDelete("{id}")]
    // public void Delete(int id)
    // {
    // }

    private IActionResult CreateInstrumentResponse(OneOf<Instrument, Error> result)
    {
        return result.Match<IActionResult>(
            instrument => new ObjectResult(instrument),
            error => new ObjectResult(error));
    }

    private IActionResult GetInstrumentResponse(OneOf<Instrument, Error> result)
    {
        return result.Match<IActionResult>(
            instrument => new ObjectResult(instrument),
            error => new ObjectResult(error));
    }

    private IActionResult QueryInstrumentsResponse(OneOf<IEnumerable<Instrument>, Error> result)
    {
        return result.Match<IActionResult>(
            instruments => new ObjectResult(instruments),
            error => new ObjectResult(error));
    }
}