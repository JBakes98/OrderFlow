using Orderflow.Domain.Models;
using Orderflow.Events.Instrument;

namespace Orderflow.Mappers.Events;

public class InstrumentToInstrumentCreatedEventMapper : IMapper<Instrument, InstrumentCreatedEvent>
{
    public InstrumentCreatedEvent Map(Instrument source)
    {
        return new InstrumentCreatedEvent(
            source.Id.ToString(),
            source.Ticker,
            source.Name,
            source.ExchangeId.ToString(),
            source.Sector);
    }
}