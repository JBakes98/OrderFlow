using OneOf;
using Orderflow.Domain.Models;
using Orderflow.Events.Instrument;

namespace Orderflow.Data.Repositories.Interfaces;

public interface IInstrumentRepository
{
    Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync();
    Task<OneOf<Instrument, Error>> GetByIdAsync(Guid id);
    Task<Error?> InsertAsync(Instrument entity, InstrumentCreatedEvent @event);
    Task<Error?> UpdateAsync(Instrument source);
}