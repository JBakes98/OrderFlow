using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Data.Entities;
using OrderFlow.Data.Repositories.Interfaces;
using OrderFlow.Domain.Models;
using OrderFlow.Extensions;
using Serilog;

namespace OrderFlow.Services;

public class InstrumentService : IInstrumentService
{
    private readonly IInstrumentRepository _repository;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IMapper<InstrumentEntity, Instrument> _instrumentDataToDomainMapper;
    private readonly IMapper<Instrument, InstrumentEntity> _instrumentDomainToDataMapper;

    public InstrumentService(
        IInstrumentRepository repository,
        IDiagnosticContext diagnosticContext,
        IMapper<InstrumentEntity, Instrument> instrumentDataToDomainMapper,
        IMapper<Instrument, InstrumentEntity> instrumentDomainToDataMapper)
    {
        _instrumentDataToDomainMapper = Guard.Against.Null(instrumentDataToDomainMapper);
        _instrumentDomainToDataMapper = Guard.Against.Null(instrumentDomainToDataMapper);
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _repository = Guard.Against.Null(repository);
    }

    public async Task<OneOf<Instrument, Error>> RetrieveInstrument(string id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        var instrument = _instrumentDataToDomainMapper.Map(result.AsT0);

        _diagnosticContext.Set($"InstrumentEntity", instrument.Ticker);

        return instrument;
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> RetrieveInstruments()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        var instruments = result.AsT0.Select(x => _instrumentDataToDomainMapper.Map(x)).ToList();

        return instruments;
    }

    public async Task<OneOf<Instrument, Error>> CreateInstrument(Instrument source)
    {
        var instrument = _instrumentDomainToDataMapper.Map(source);
        var result = await _repository.InsertAsync(instrument, default);

        if (result.IsT1)
            return result.AsT1;

        return _instrumentDataToDomainMapper.Map(result.AsT0);
    }
}