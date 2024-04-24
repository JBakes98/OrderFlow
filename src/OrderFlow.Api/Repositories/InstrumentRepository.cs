using System.Net;
using Amazon.DynamoDBv2.DataModel;
using OneOf;
using OrderFlow.Domain;
using OrderFlow.Events;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public class InstrumentRepository : IRepository<Instrument>
{
    private readonly IDynamoDBContext _context;

    public InstrumentRepository(IDynamoDBContext context)
    {
        _context = context;
    }

    public void Dispose()
    {
        _context.Dispose();
    }

    public async Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync()
    {
        var conditions = new List<ScanCondition>();
        var results = await _context.ScanAsync<Instrument>(conditions).GetRemainingAsync();

        return results;
    }

    public async Task<OneOf<Instrument, Error>> GetByIdAsync(string id)
    {
        var result = await _context.LoadAsync<Instrument>(id);

        if (result == null)
            return new Error(HttpStatusCode.NotFound, ErrorCodes.InstrumentNotFound);

        return result;
    }

    public async Task<OneOf<Instrument, Error>> InsertAsync(Instrument source, CancellationToken cancellationToken)
    {
        await _context.SaveAsync(source, cancellationToken);

        return source;
    }

    public Task DeleteAsync(Instrument source)
    {
        throw new NotImplementedException();
    }

    public Task UpdateAsync(Instrument source)
    {
        throw new NotImplementedException();
    }
}