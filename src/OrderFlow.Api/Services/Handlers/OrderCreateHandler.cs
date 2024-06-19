using System.Net;
using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Contexts;
using OrderFlow.Contracts.Requests;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Extensions;
using OrderFlow.Models;

namespace OrderFlow.Services.Handlers;

public class OrderCreateHandler : IHandler<CreateOrder, Order>
{
    private readonly IMapper<CreateOrder, Order> _createOrderToOrderMapper;
    private readonly IMapper<Order, OrderCreatedEvent> _orderToOrderCreatedEventMapper;
    private readonly IMapper<BaseOrderEvent, Event> _orderEventToEventMapper;
    private readonly AppDbContext _context;
    private readonly IInstrumentService _instrumentService;
    private readonly IEnqueueService _enqueueService;

    public OrderCreateHandler(
        IMapper<CreateOrder, Order> createOrderToOrderMapper,
        IMapper<Order, OrderCreatedEvent> orderToOrderCreatedEventMapper,
        IInstrumentService instrumentService,
        IMapper<BaseOrderEvent, Event> orderEventToEventMapper,
        AppDbContext context, 
        IEnqueueService enqueueService)
    {
        _enqueueService = Guard.Against.Null(enqueueService);
        _createOrderToOrderMapper = Guard.Against.Null(createOrderToOrderMapper);
        _orderToOrderCreatedEventMapper = Guard.Against.Null(orderToOrderCreatedEventMapper);
        _instrumentService = Guard.Against.Null(instrumentService);
        _orderEventToEventMapper = Guard.Against.Null(orderEventToEventMapper);
        _context = Guard.Against.Null(context);
    }
    public async Task<OneOf<Order, Error>> HandleAsync(CreateOrder request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.RetrieveInstrument(request.InstrumentId.ToString());

        if (instrument.IsT1)
            return instrument.AsT1;

        var order = _createOrderToOrderMapper.Map(request);

        await using var transaction = await _context.Database.BeginTransactionAsync(cancellationToken);

        try
        {
            _context.Orders.Add(order);

            var orderEvent = _orderToOrderCreatedEventMapper.Map(order);
            var @event = _orderEventToEventMapper.Map(orderEvent);
            await _enqueueService.PublishEvent(@event, cancellationToken);

            await _context.SaveChangesAsync(cancellationToken);

            await transaction.CommitAsync(cancellationToken);

            return order;
        }
        catch (Exception e)
        {
            return new Error(HttpStatusCode.UnprocessableEntity, ErrorCodes.OrderNotFound);
        }
    }
}