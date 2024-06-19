using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Contexts;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public class EventRepository : IEventRepository
{
    private readonly AppDbContext _context;

    public EventRepository(AppDbContext context)
    {
        _context = Guard.Against.Null(context);
    }

    public async Task<OneOf<IEnumerable<Event>, Error>> QueryAsync(string streamId = null)
    {
        var result = await _context.Events.Where(x => x.StreamId == streamId).ToListAsync();

        return result;
    }

    public async Task<OneOf<IEnumerable<Event>, Error>> QueryAsync()
    {
        var result = await _context.Events.ToListAsync();

        return result;
    }

    public async Task<OneOf<Event, Error>> GetByIdAsync(string id)
    {
        var result = await _context.Events.FindAsync(id);

        if (result == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        return result;
    }

    public async Task<OneOf<Event, Error>> InsertAsync(Event source, CancellationToken cancellationToken)
    {
        _context.Events.Add(source);
        await _context.SaveChangesAsync(cancellationToken);

        return source;
    }

    public Task DeleteAsync(Event source)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Event source)
    {
        throw new NotImplementedException();
    }
}