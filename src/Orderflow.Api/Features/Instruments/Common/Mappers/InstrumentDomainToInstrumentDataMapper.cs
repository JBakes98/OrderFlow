using Orderflow.Features.Common;

namespace Orderflow.Features.Instruments.Common.Mappers;

public class InstrumentDomainToInstrumentDataMapper : IMapper<Instrument, InstrumentEntity>
{
    public InstrumentEntity Map(Instrument source)
    {
        return new InstrumentEntity(
            source.Id,
            source.Ticker,
            source.Name,
            source.Sector,
            source.ExchangeId);
    }
}