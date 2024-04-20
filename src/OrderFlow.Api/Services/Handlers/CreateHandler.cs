using Amazon.DynamoDBv2.DataModel;
using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Extensions;
using OrderFlow.Models;

namespace OrderFlow.Services.Handlers;

public class CreateHandler : IOrderHandler<CreateOrder>
{
    private readonly IMapper<CreateOrder, Order> _createOrderToOrderMapper;
    private readonly IDynamoDBContext _context;

    public CreateHandler(
        IMapper<CreateOrder, Order> createOrderToOrderMapper,
        IDynamoDBContext context)
    {
        _createOrderToOrderMapper = createOrderToOrderMapper;
        _context = context;
    }
    public async Task<OneOf<Order, Error>> HandleAsync(CreateOrder request, CancellationToken cancellationToken)
    {
        var order = _createOrderToOrderMapper.Map(request);
        
        await _context.SaveAsync(order, cancellationToken);
        
        return order;
    }
}