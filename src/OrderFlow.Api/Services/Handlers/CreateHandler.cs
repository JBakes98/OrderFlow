using OneOf;
using OrderFlow.Contracts.Requests;
using OrderFlow.Models;

namespace OrderFlow.Services.Handlers;

public class CreateHandler
{
    public async Task<OneOf<Order, Error>> HandleAsync(CreateOrder request, CancellationToken cancellationToken)
    {
        
    }
}