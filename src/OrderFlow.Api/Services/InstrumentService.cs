using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Services;

public class InstrumentService : IInstrumentService
{
    private readonly IRepository<Instrument> _repository;

    public InstrumentService(IRepository<Instrument> repository)
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

        return result.AsT0.ToList();
    }
    
    public async Task<OneOf<Instrument, Error>> CreateInstrument(Instrument instrument)
    {
        var result = await _repository.InsertAsync(instrument, default);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }
}