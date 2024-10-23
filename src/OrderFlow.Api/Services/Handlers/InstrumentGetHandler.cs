using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Domain.Models;

namespace OrderFlow.Services.Handlers;

public class InstrumentGetHandler : IHandler<string, Instrument>
{
    private readonly IInstrumentService _instrumentService;

    public InstrumentGetHandler(
        IInstrumentService instrumentService)
    {
        _instrumentService = Guard.Against.Null(instrumentService);
    }

    public async Task<OneOf<Instrument, Error>> HandleAsync(string id, CancellationToken cancellationToken)
    {
        var result = await _instrumentService.RetrieveInstrument(id);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }
}