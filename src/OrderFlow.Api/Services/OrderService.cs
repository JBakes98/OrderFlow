using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using OrderFlow.Contexts;
using OrderFlow.Domain;
using OrderFlow.Models;

namespace OrderFlow.Services;

public class OrderService : IOrderService
{
    private readonly AppDbContext _context;

    public OrderService(
        AppDbContext context)
    {
        _context = Guard.Against.Null(context);
    }

    public async Task<OneOf<Order, Error>> RetrieveOrder(string id)
    {
        var order = await _context.Orders.FindAsync(id);

        if (order == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        return order;
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> RetrieveOrders()
    {
        var result = await _context.Orders.ToListAsync();

        return result;
    }
}