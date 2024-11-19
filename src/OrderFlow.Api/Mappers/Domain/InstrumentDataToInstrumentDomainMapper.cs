using OrderFlow.Data.Entities;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Domain;

public class InstrumentDataToInstrumentDomainMapper : IMapper<InstrumentEntity, OrderFlow.Domain.Models.Instrument>
{
    public OrderFlow.Domain.Models.Instrument Map(InstrumentEntity source)
    {
        return new OrderFlow.Domain.Models.Instrument(
            source.Id,
            source.Ticker,
            source.Name,
            source.Sector,
            source.Exchange);
    }
}