using OrderFlow.Domain.Models;
using OrderFlow.Events;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Events;

public class InstrumentToInstrumentCreatedEventMapper : IMapper<Instrument, InstrumentCreatedEvent>
{
    public InstrumentCreatedEvent Map(Instrument source)
    {
        return new InstrumentCreatedEvent(
            instrumentId: source.Id,
            ticker: source.Ticker,
            name: source.Name,
            exchange: source.Exchange,
            sector: source.Sector);
    }
}