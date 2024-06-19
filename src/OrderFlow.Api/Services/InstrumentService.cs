using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Models;
using OrderFlow.Repositories;
using Instrument = OrderFlow.Models.Instrument;

namespace OrderFlow.Services;

public class InstrumentService : IInstrumentService
{
    private readonly IInstrumentRepository _repository;

    public InstrumentService(
        IInstrumentRepository repository)
    {
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Instrument, Error>> RetrieveInstrument(string id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> RetrieveInstruments()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var instruments = result.AsT0;

        return instruments.ToList();
    }
}