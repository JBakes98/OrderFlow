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

public class OrderRepository : IOrderRepository
{
    private readonly OrderflowDbContext _context;
    private readonly IMapper<Order, OrderEntity> _orderDomainToEntityMapper;
    private readonly IMapper<OrderEntity, Order> _orderEntityToDomainMapper;

    public OrderRepository(OrderflowDbContext context,
        IMapper<Order, OrderEntity> orderDomainToEntityMapper,
        IMapper<OrderEntity, Order> orderEntityToDomainMapper)
    {
        _orderEntityToDomainMapper = Guard.Against.Null(orderEntityToDomainMapper);
        _orderDomainToEntityMapper = Guard.Against.Null(orderDomainToEntityMapper);
        _context = Guard.Against.Null(context);
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> QueryAsync()
    {
        var ordersResult = await _context.Orders.ToListAsync();
        var orders = ordersResult.Select(x => _orderEntityToDomainMapper.Map(x)).ToList();

        return orders;
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(string instrumentId)
    {
        var ordersResult = await _context.Orders
            .Where(x => x.InstrumentId.Equals(instrumentId))
            .ToListAsync();

        var orders = ordersResult.Select(x => _orderEntityToDomainMapper.Map(x)).ToList();

        return orders;
    }

    public async Task<OneOf<Order, Error>> GetByIdAsync(string id)
    {
        var orderResult = await _context.Orders.FindAsync(id);

        if (orderResult == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        var order = _orderEntityToDomainMapper.Map(orderResult);

        return order;
    }

    public async Task<OneOf<Order, Error>> InsertAsync(Order source, CancellationToken cancellationToken)
    {
        var orderEntity = _orderDomainToEntityMapper.Map(source);

        var result = await _context.AddAsync(orderEntity, cancellationToken);
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