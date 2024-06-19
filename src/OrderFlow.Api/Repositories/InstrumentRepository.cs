using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Contexts;
using OrderFlow.Domain;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public class InstrumentRepository : IInstrumentRepository
{
    private readonly AppDbContext _context;

    public InstrumentRepository(AppDbContext context)
    {
        _context = Guard.Against.Null(context);
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync()
    {
        var results = await _context.Instruments.ToListAsync();

        return results;
    }

    public async Task<OneOf<Instrument, Error>> GetByIdAsync(string id)
    {
        var result = await _context.Instruments.FindAsync(id);

        if (result == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        return result;
    }

    public async Task<OneOf<Instrument, Error>> InsertAsync(Instrument source, CancellationToken cancellationToken)
    {
        _context.Instruments.Add(source);
        await _context.SaveChangesAsync(cancellationToken);

        return source;
    }
}