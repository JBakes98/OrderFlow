using Amazon.DynamoDBv2.DataModel;
using OneOf;
using OrderFlow.Models;

namespace OrderFlow.Services.Handlers;

public class GetHandler : IOrderHandler<Guid>
{
    private readonly IDynamoDBContext _context;

    public GetHandler(IDynamoDBContext context)
    {
        _context = context;
    }

    public async Task<OneOf<Order, Error>> HandleAsync(Guid request, CancellationToken cancellationToken)
    {
        var order = await _context.LoadAsync<Order>(request.ToString(), cancellationToken);

        return order;
    }
}