using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Extensions;
using OrderFlow.Models;
using OrderFlow.Repositories;

namespace OrderFlow.Services.Handlers;

public class OrderCreateHandler : IHandler<CreateOrder, Order>
{
    private readonly IMapper<CreateOrder, Order> _createOrderToOrderMapper;
    private readonly IRepository<Order> _repository;
    private readonly IInstrumentService _instrumentService;

    public OrderCreateHandler(
        IMapper<CreateOrder, Order> createOrderToOrderMapper,
        IRepository<Order> repository,
        IInstrumentService instrumentService)
    {
        _createOrderToOrderMapper = createOrderToOrderMapper;
        _repository = repository;
        _instrumentService = instrumentService;
    }
    public async Task<OneOf<Order, Error>> HandleAsync(CreateOrder request, CancellationToken cancellationToken)
    {
        var instrument = await _instrumentService.RetrieveInstrument(request.InstrumentId.ToString());

        if (instrument.IsT1)
            return instrument.AsT1;

        var order = _createOrderToOrderMapper.Map(request);
        
        await _repository.InsertAsync(order, cancellationToken);
        
        return order;
    }
}