using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Data.DbContext;
using OrderFlow.Data.Entities;
using OrderFlow.Data.Repositories.Interfaces;
using OrderFlow.Domain;
using OrderFlow.Domain.Models;
using OrderFlow.Extensions;

namespace OrderFlow.Data.Repositories;

public class InstrumentRepository : IInstrumentRepository
{
    private readonly OrderflowDbContext _context;
    private readonly IMapper<Instrument, InstrumentEntity> _instrumentDomainToDataMapper;
    private readonly IMapper<InstrumentEntity, Instrument> _instrumentDataToDomainMapper;

    public InstrumentRepository(OrderflowDbContext context,
        IMapper<Instrument, InstrumentEntity> instrumentDomainToDataMapper,
        IMapper<InstrumentEntity, Instrument> instrumentDataToDomainMapper)
    {
        _instrumentDataToDomainMapper = Guard.Against.Null(instrumentDataToDomainMapper);
        _instrumentDomainToDataMapper = Guard.Against.Null(instrumentDomainToDataMapper);
        _context = Guard.Against.Null(context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync()
    {
        var result = await _context.Instruments.ToListAsync();

        var instruments = result.Select(x =>
                _instrumentDataToDomainMapper.Map(x))
            .ToList();

        return instruments;
    }

    public async Task<OneOf<Instrument, Error>> GetByIdAsync(string id)
    {
        var instrumentEntity = await _context.Instruments.FindAsync(id);

        if (instrumentEntity == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        var instrument = _instrumentDataToDomainMapper.Map(instrumentEntity);

        return instrument;
    }

    public async Task<OneOf<Instrument, Error>> InsertAsync(Instrument source,
        CancellationToken cancellationToken)
    {
        var instrumentEntity = _instrumentDomainToDataMapper.Map(source);

        var result = await _context.AddAsync(instrumentEntity, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return source;
    }

    public Task DeleteAsync(Instrument source)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Instrument source)
    {
        throw new NotImplementedException();
    }
}