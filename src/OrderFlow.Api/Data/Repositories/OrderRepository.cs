using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Data.DbContext;
using OrderFlow.Data.Repositories.Interfaces;
using OrderFlow.Domain;
using OrderFlow.Domain.Models;

namespace OrderFlow.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderflowDbContext _context;

    public OrderRepository(OrderflowDbContext context)
    {
        _context = Guard.Against.Null(context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> QueryAsync()
    {
        var orders = await _context.Orders.ToListAsync();
        return orders;
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(Guid instrumentId)
    {
        var orders = await _context.Orders
            .Where(x => x.InstrumentId.Equals(instrumentId))
            .ToListAsync();

        return orders;
    }

    public async Task<OneOf<Order, Error>> GetByIdAsync(string id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        return order;
    }

    public async Task<OneOf<Order, Error>> InsertAsync(Order source, CancellationToken cancellationToken)
    {
        var result = await _context.AddAsync(source, cancellationToken);
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