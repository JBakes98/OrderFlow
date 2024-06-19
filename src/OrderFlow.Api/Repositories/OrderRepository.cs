using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Contexts;
using OrderFlow.Domain;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly AppDbContext _context;

    public OrderRepository(AppDbContext context)
    {
        _context = Guard.Against.Null(context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> QueryAsync()
    {
        var result = await _context.Orders.ToListAsync();

        return result;
    }

    public Task<OneOf<IEnumerable<Order>, Error>> QueryAsync(string streamId)
    {
        throw new NotImplementedException();
    }

    public async Task<OneOf<Order, Error>> GetByIdAsync(string id)
    {
        var result = await _context.Orders.FindAsync(id);

        if (result == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        return result;
    }

    public async Task<OneOf<Order, Error>> InsertAsync(Order source, CancellationToken cancellationToken)
    {
        _context.Orders.Add(source);
        await _context.SaveChangesAsync(cancellationToken);

        return source;
    }

    public Task DeleteAsync(Order source)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order source)
    {
        throw new NotImplementedException();
    }
}