using Orderflow.Data.Entities;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Domain;

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