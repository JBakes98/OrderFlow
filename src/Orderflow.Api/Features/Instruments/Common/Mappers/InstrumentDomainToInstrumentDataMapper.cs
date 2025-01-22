using Orderflow.Data.Entities;
using Orderflow.Domain.Models;

namespace Orderflow.Mappers.Data;

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