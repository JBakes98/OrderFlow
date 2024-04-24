using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Events;
using OrderFlow.Extensions;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Services.Handlers;

public class OrderCreateHandler : IHandler<CreateOrder, Order>
{
    private readonly IMapper<CreateOrder, Order> _createOrderToOrderMapper;
    private readonly IMapper<Order, OrderCreatedEvent> _orderToOrderCreatedEventMapper;
    private readonly IRepository<Order> _repository;
    private readonly IRepository<DomainEvent> _domainRepository;
    private readonly IInstrumentService _instrumentService;

    public OrderCreateHandler(
        IMapper<CreateOrder, Order> createOrderToOrderMapper,
        IMapper<Order, OrderCreatedEvent> orderToOrderCreatedEventMapper,
        IRepository<Order> repository,
        IRepository<DomainEvent> domainRepository,
        IInstrumentService instrumentService)
    {
        _createOrderToOrderMapper = createOrderToOrderMapper;
        _orderToOrderCreatedEventMapper = orderToOrderCreatedEventMapper;
        _repository = repository;
        _domainRepository = domainRepository;
        _instrumentService = instrumentService;
    }
    public async Task<OneOf<Order, Error>> HandleAsync(CreateOrder request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.RetrieveInstrument(request.InstrumentId.ToString());

        if (instrument.IsT1)
            return instrument.AsT1;

        var order = _createOrderToOrderMapper.Map(request);

        await _repository.InsertAsync(order, cancellationToken);

        var @event = _orderToOrderCreatedEventMapper.Map(order);
        await _domainRepository.InsertAsync(@event, cancellationToken);

        return order;
    }
}