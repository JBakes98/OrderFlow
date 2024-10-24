using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Data.DbContext;
using OrderFlow.Data.Entities;
using OrderFlow.Data.Repositories.Interfaces;
using OrderFlow.Domain;
using OrderFlow.Domain.Models;

namespace OrderFlow.Data.Repositories;

public class InstrumentRepository : IInstrumentRepository
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

    public async Task<OneOf<IEnumerable<InstrumentEntity>, Error>> QueryAsync()
    {
        var instruments = await _context.Instruments.ToListAsync();

        return instruments;
    }

    public async Task<OneOf<InstrumentEntity, Error>> GetByIdAsync(string id)
    {
        var instrument = await _context.Instruments.FindAsync(id);

        if (instrument == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        return instrument;
    }

    public async Task<OneOf<InstrumentEntity, Error>> InsertAsync(InstrumentEntity source,
        CancellationToken cancellationToken)
    {
        var result = await _context.AddAsync(source, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return source;
    }

    public Task DeleteAsync(InstrumentEntity source)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(InstrumentEntity source)
    {
        throw new NotImplementedException();
    }
}