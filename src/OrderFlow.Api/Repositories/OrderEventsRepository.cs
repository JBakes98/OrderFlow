using System.Net;
using Amazon.DynamoDBv2.DataModel;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public class OrderEventsRepository : IRepository<DomainEvent>, IDisposable
{
    private readonly IDynamoDBContext _context;

    public OrderEventsRepository(IDynamoDBContext context)
    {
        _context = context;
    }
    
    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<OneOf<IEnumerable<DomainEvent>, Error>> QueryAsync()
    {
        var conditions = new List<ScanCondition>();
        var results = await _context.ScanAsync<DomainEvent>(conditions).GetRemainingAsync();

        return results;
    }

    public async Task<OneOf<DomainEvent, Error>> GetByIdAsync(string id)
    {
        var result = await _context.LoadAsync<DomainEvent>(id);

        if (result == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.OrderNotFound);

        return result;
    }

    public async Task<OneOf<DomainEvent, Error>> InsertAsync(DomainEvent source, CancellationToken cancellationToken)
    {
        await _context.SaveAsync(source, cancellationToken);

        return source;
    }

    public Task DeleteAsync(DomainEvent source)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(DomainEvent source)
    {
        throw new NotImplementedException();
    }
}