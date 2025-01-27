using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;
using Orderflow.Features.Instruments.CreateInstrument.Events;

namespace Orderflow.Features.Instruments.CreateInstrument.Mappers;

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