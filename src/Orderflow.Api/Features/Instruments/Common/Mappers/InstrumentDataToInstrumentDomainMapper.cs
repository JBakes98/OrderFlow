using Orderflow.Features.Common;

namespace Orderflow.Features.Instruments.Common.Mappers;

public class InstrumentDataToInstrumentDomainMapper : IMapper<InstrumentEntity, Instrument>
{
    public Instrument Map(InstrumentEntity source)
    {
        return new Instrument(
            source.Id,
            source.Ticker,
            source.Name,
            source.Sector,
            source.ExchangeId);
    }
}