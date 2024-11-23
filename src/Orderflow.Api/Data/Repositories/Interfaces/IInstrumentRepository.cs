using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Events;

namespace Orderflow.Data.Repositories.Interfaces;

public interface IInstrumentRepository
{
    Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync();
    Task<OneOf<Instrument, Error>> GetByIdAsync(string id);
    Task<Error?> InsertAsync(Instrument entity, InstrumentCreatedEvent @event);
    Task<Error?> UpdateAsync(Instrument source);
}