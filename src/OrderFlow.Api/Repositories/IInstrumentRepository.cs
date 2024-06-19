using OneOf;
using OrderFlow.Models;

namespace OrderFlow.Repositories;

public interface IInstrumentRepository
{
    Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync();
    Task<OneOf<Instrument, Error>> GetByIdAsync(string id);
    Task<OneOf<Instrument, Error>> InsertAsync(Instrument source, CancellationToken cancellationToken);
}