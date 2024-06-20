using Ardalis.GuardClauses;
using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Extensions;
using OrderFlow.Models;

namespace OrderFlow.Services.Handlers;

public class OrderCreateHandler : IHandler<CreateOrder, Order>
{
    private readonly IMapper<CreateOrder, Order> _createOrderToOrderMapper;
    private readonly IInstrumentService _instrumentService;
    private readonly IOrderService _orderService;

    public OrderCreateHandler(
        IMapper<CreateOrder, Order> createOrderToOrderMapper,
        IInstrumentService instrumentService,
        IOrderService orderService)
    {
        _createOrderToOrderMapper = Guard.Against.Null(createOrderToOrderMapper);
        _instrumentService = Guard.Against.Null(instrumentService);
        _orderService = Guard.Against.Null(orderService);
    }

    public async Task<OneOf<Order, Error>> HandleAsync(CreateOrder request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.RetrieveInstrument(request.InstrumentId.ToString());

        if (instrument.IsT1)
            return instrument.AsT1;

        var order = _createOrderToOrderMapper.Map(request);

        var result = await _orderService.CreateOrder(order);

        if (result.IsT1)
            return result.AsT1;

        return result.AsT0;
    }
}