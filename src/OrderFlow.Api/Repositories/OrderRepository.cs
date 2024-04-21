using System.Net;
using Amazon.DynamoDBv2.DataModel;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public class OrderRepository : IRepository<Order>, IDisposable
{
    private readonly IDynamoDBContext _context;

    public OrderRepository(IDynamoDBContext context)
    {
        _context = context;
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public Task<IEnumerable<Order>> Get()
    {
        throw new NotImplementedException();
    }

    public async Task<OneOf<Order, Error>> GetById(string id)
    {
        var result = await _context.LoadAsync<Order>(id);

        if (result == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        return result;
    }

    public async Task Insert(Order source, CancellationToken cancellationToken)
    {
        await _context.SaveAsync(source, cancellationToken);
    }

    public Task Delete(Order source)
    {
        throw new NotImplementedException();
    }

    public Task Update(Order source)
    {
        throw new NotImplementedException();
    }
}