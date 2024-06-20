using System.Net;
using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Extensions;
using OrderFlow.Models;
using OrderFlow.Repositories;
using Error = OrderFlow.Models.Error;

namespace OrderFlow.Services;

public class OrderService : IOrderService
{
    private readonly IRepository<Order> _repository;
    private readonly IEnqueueService _enqueueService;
    private readonly IMapper<Order, OrderCreatedEvent> _orderToOrderCreatedEventMapper;
    private readonly IMapper<BaseOrderEvent, Event> _orderEventToEventMapper;


    public OrderService(IRepository<Order> repository, 
        IEnqueueService enqueueService, 
        IMapper<Order, OrderCreatedEvent> orderToOrderCreatedEventMapper, 
        IMapper<BaseOrderEvent, Event> orderEventToEventMapper)
    {
        _orderEventToEventMapper = Guard.Against.Null(orderEventToEventMapper);
        _orderToOrderCreatedEventMapper = Guard.Against.Null(orderToOrderCreatedEventMapper);
        _enqueueService = Guard.Against.Null(enqueueService);
        _repository = Guard.Against.Null(repository);
    }


    public async Task<OneOf<Order, Error>> RetrieveOrder(string id)
    {
        var result = await _repository.GetByIdAsync(id);

        if (result.IsT1)
            return result.AsT1;

        return result;
    }

    public async Task<OneOf<IEnumerable<Order>, Error>> RetrieveOrders()
    {
        var result = await _repository.QueryAsync();

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0.ToList();
    }

    public async Task<OneOf<Order, Error>> CreateOrder(Order order)
    {
        var saveResult = await _repository.InsertAsync(order, default);

        if (saveResult.IsT1)
            return saveResult.AsT1;
        
        var orderEvent = _orderToOrderCreatedEventMapper.Map(order);
        var @event = _orderEventToEventMapper.Map(orderEvent);

        var eventPublished = await _enqueueService.PublishEvent(@event);

        if (!eventPublished)
            return new Error(HttpStatusCode.InternalServerError, ErrorCodes.EventCouldNotBePublished);

        return order;
    }
}