using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Events;

public class InstrumentToInstrumentCreatedEventMapper : IMapper<Instrument, InstrumentCreatedEvent>
{
    public InstrumentCreatedEvent Map(Instrument source)
    {
        return new InstrumentCreatedEvent(
            source.Id,
            source.Ticker,
            source.Name,
            source.Exchange,
            source.Sector);
    }
}