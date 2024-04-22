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

    public async Task<OneOf<IEnumerable<Order>, Error>> QueryAsync()
    {
        var conditions = new List<ScanCondition>();
        var results = await _context.ScanAsync<Order>(conditions).GetRemainingAsync();

        return results;
    }

    public async Task<OneOf<Order, Error>> GetByIdAsync(string id)
    {
        var result = await _context.LoadAsync<Order>(id);

        if (result == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        return result;
    }

    public async Task<OneOf<Order, Error>> InsertAsync(Order source, CancellationToken cancellationToken)
    {
        await _context.SaveAsync(source, cancellationToken);

        return source;
    }

    public Task DeleteAsync(Order source)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Order source)
    {
        throw new NotImplementedException();
    }
}