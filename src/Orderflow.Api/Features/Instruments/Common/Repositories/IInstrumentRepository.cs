using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Instruments.CreateInstrument.Events;

namespace Orderflow.Features.Instruments.Common.Repositories;

public interface IInstrumentRepository
{
    Task<OneOf<IEnumerable<Instrument>, Error>> QueryAsync();
    Task<OneOf<Instrument, Error>> GetByIdAsync(Guid id);
    Task<Error?> InsertAsync(Instrument entity, InstrumentCreatedEvent @event);
    Task<Error?> UpdateAsync(Instrument source);
}