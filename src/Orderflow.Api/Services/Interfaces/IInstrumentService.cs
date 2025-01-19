using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Services.Interfaces;

public interface IInstrumentService
{
    Task<OneOf<Instrument, Error>> RetrieveInstrument(Guid id);
    Task<OneOf<IEnumerable<Instrument>, Error>> RetrieveInstruments();
    Task<OneOf<Instrument, Error>> CreateInstrument(Instrument instrument);
}