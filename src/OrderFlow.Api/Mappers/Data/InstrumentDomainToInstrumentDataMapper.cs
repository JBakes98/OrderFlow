using OrderFlow.Data.Entities;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Data;

public class InstrumentDomainToInstrumentDataMapper : IMapper<OrderFlow.Domain.Models.Instrument, InstrumentEntity>
{
    public InstrumentEntity Map(OrderFlow.Domain.Models.Instrument source)
    {
        return new InstrumentEntity(
            source.Id,
            source.Ticker,
            source.Name,
            source.Sector,
            source.Exchange);
    }
}