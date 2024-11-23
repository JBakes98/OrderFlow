using System.Net;
using Ardalis.GuardClauses;
using Microsoft.EntityFrameworkCore;
using OneOf;
using Orderflow.Data.DbContext;
using Orderflow.Data.Entities;
using Orderflow.Data.Repositories.Interfaces;
using Orderflow.Domain;
using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Extensions;
using Serilog;

namespace Orderflow.Data.Repositories;

public class OrderRepository : IOrderRepository
{
    private readonly OrderflowDbContext _context;
    private readonly IDiagnosticContext _diagnosticContext;
    private readonly IEventMapperFactory _eventMapperFactory;
    private readonly IMapper<Order, OrderEntity> _orderDomainToEntityMapper;
    private readonly IMapper<OrderEntity, Order> _orderEntityToDomainMapper;

    public OrderRepository(OrderflowDbContext context,
        IMapper<Order, OrderEntity> orderDomainToEntityMapper,
        IMapper<OrderEntity, Order> orderEntityToDomainMapper,
        IEventMapperFactory eventMapperFactory,
        IDiagnosticContext diagnosticContext)
    {
        _diagnosticContext = Guard.Against.Null(diagnosticContext);
        _eventMapperFactory = Guard.Against.Null(eventMapperFactory);
        _orderEntityToDomainMapper = Guard.Against.Null(orderEntityToDomainMapper);
        _orderDomainToEntityMapper = Guard.Against.Null(orderDomainToEntityMapper);
        _context = Guard.Against.Null(context);
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> QueryAsync()
    {
        try
        {
            var ordersResult = await _context.Orders.ToListAsync();
            var orders = ordersResult.Select(x => _orderEntityToDomainMapper.Map(x)).ToList();

            return orders;
        }
        catch (Exception e)
        {
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.OrderNotFound);
        }
    }

    public async Task<OneOf<Order, Error>> GetByIdAsync(string id)
    {
        var orderResult = await _context.Orders.FindAsync(id);

        if (orderResult == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        var order = _orderEntityToDomainMapper.Map(orderResult);

        return order;
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> GetInstrumentOrders(string instrumentId)
    {
        var ordersResult = await _context.Orders
            .Where(x => x.InstrumentId.Equals(instrumentId))
            .ToListAsync();

        var orders = ordersResult.Select(x => _orderEntityToDomainMapper.Map(x)).ToList();

        return orders;
    }

    public async Task<Error?> InsertAsync(Order order, OrderRaisedEvent @event)
    {
        var outboxEvent = _eventMapperFactory.MapEvent(@event);
        var entity = _orderDomainToEntityMapper.Map(order);

        await using var transaction = await _context.Database.BeginTransactionAsync();
        try
        {
            _context.Set<OrderEntity>().Add(entity);
            _context.Set<OutboxEvent>().Add(outboxEvent);

            await _context.SaveChangesAsync();
            await transaction.CommitAsync();

            return null;
        }
        catch (Exception e)
        {
            await transaction.RollbackAsync();

            _diagnosticContext.Set("Order.Error", "Failed to raise order");

            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.OrderCouldNotBeCreated);
        }
    }

    public Task<Error?> UpdateAsync(Order source)
    {
        throw new NotImplementedException();
    }
}