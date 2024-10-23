using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public class InstrumentRepository : IRepository<Instrument>
{
    private readonly OrderflowDbContext _context;

    public InstrumentRepository(OrderflowDbContext context)
    {
        _context = Guard.Against.Null(context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync()
    {
        var instruments = await _context.Instruments.ToListAsync();

        return instruments;
    }

    public async Task<OneOf<Instrument, Error>> GetByIdAsync(string id)
    {
        var instrument = await _context.Instruments.FindAsync(id);

        return instrument == null
            ? new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound)
            : instrument;
    }

    public async Task<OneOf<Instrument, Error>> InsertAsync(Instrument source, CancellationToken cancellationToken)
    {
        var result = await _context.AddAsync(source, cancellationToken);
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