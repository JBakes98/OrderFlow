using OneOf;
using OrderFlow.Domain.Models;
using OrderFlow.Events;

namespace OrderFlow.Data.Repositories.Interfaces;

public interface IInstrumentRepository
{
    Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync();
    Task<OneOf<Instrument, Error>> GetByIdAsync(string id);
    Task<Error?> InsertAsync(Instrument entity, InstrumentCreatedEvent @event);
    Task<Error?> UpdateAsync(Instrument source);
}