using System.Text.Json;
using Amazon.DynamoDBv2;
using Amazon.DynamoDBv2.DataModel;
using Amazon.DynamoDBv2.DocumentModel;
using OneOf;
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public class OrderEventsRepository : IRepository<Event>
{
    private readonly IDynamoDBContext _context;
    private readonly IAmazonDynamoDB _dynamoDBClient;

    public OrderEventsRepository(IAmazonDynamoDB dynamoDbClient)
    {
        _dynamoDBClient = dynamoDbClient;
        _context = new DynamoDBContext(_dynamoDBClient);
    }

    public void Dispose()
    {
        _context.Dispose();
    }


    public async Task<OneOf<IEnumerable<Event>, Error>> QueryAsync()
    {
        var conditions = new List<ScanCondition>();
        var results = await _context.ScanAsync<Event>(conditions).GetRemainingAsync();

        return results;
    }

    public Task<OneOf<Event, Error>> GetByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<OneOf<Event, Error>> InsertAsync(Event source, CancellationToken cancellationToken)
    {
        var json = JsonSerializer.Serialize(source);

        var doc = Document.FromJson(json);
        var table = Table.LoadTable(_dynamoDBClient, "OrderEvents");

        await table.PutItemAsync(doc, cancellationToken);

        return source;
    }

    public Task DeleteAsync(Event source)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Event source)
    {
        throw new NotImplementedException();
    }
}