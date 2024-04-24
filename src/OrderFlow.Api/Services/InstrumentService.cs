using OneOf;
using OrderFlow.Models;
using OrderFlow.Repositories;
using Instrument = OrderFlow.Models.Instrument;

namespace OrderFlow.Services;

public class InstrumentService : IInstrumentService
{
    private readonly IRepository<Instrument> _repository;

    public InstrumentService(IRepository<Instrument> repository)
    {
        _repository = repository;
    }

    public async Task<OneOf<Instrument, Error>> RetrieveInstrument(string id)
    {
        var instrument = await _repository.GetByIdAsync(id);

        if (instrument.IsT1)
            return instrument.AsT1;

        return instrument;
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> RetrieveInstruments()
    {
        var result = await _repository.QueryAsync();

        return result;
    }
}