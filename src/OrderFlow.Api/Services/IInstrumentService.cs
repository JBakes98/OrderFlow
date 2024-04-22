using OneOf;
using OrderFlow.Models;

namespace OrderFlow.Services;

public interface IInstrumentService
{
    Task<OneOf<Instrument, Error>> RetrieveInstrument(string id);
    Task<OneOf<IEnumerable<Instrument>, Error>> RetrieveInstruments();
}