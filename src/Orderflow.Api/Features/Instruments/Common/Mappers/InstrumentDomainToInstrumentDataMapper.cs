using Orderflow.Common.Mappers;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.Common.Repositories;

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