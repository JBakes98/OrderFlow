using OneOf;
using OrderFlow.Domain.Models;

namespace OrderFlow.Services;

public interface IInstrumentService
{
    Task<OneOf<Instrument, Error>> RetrieveInstrument(string id);
    Task<OneOf<IEnumerable<Instrument>, Error>> RetrieveInstruments();
    Task<OneOf<Instrument, Error>> CreateInstrument(Instrument instrument);
}