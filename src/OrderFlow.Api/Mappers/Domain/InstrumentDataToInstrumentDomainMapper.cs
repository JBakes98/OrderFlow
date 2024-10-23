using OrderFlow.Data.Entities;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Domain;

public class InstrumentDataToInstrumentDomainMapper : IMapper<Instrument, OrderFlow.Domain.Models.Instrument>
{
    public OrderFlow.Domain.Models.Instrument Map(Instrument source)
    {
        return new OrderFlow.Domain.Models.Instrument(
            source.Ticker,
            source.Name,
            source.Sector,
            source.Exchange);
    }
}