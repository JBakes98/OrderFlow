using Orderflow.Features.Common;
using Orderflow.Features.Instruments.Common;
using Orderflow.Features.Instruments.GetInstrument.Contracts;

namespace Orderflow.Features.Instruments.GetInstrument.Mappers;

public class InstrumentToGetInstrumentResponseMapper : IMapper<Instrument, GetInstrumentResponse>
{
    public GetInstrumentResponse Map(Instrument source)
    {
        return new GetInstrumentResponse(
            id: source.Id.ToString(),
            ticker: source.Ticker,
            name: source.Name,
            sector: source.Sector,
            exchange: source.ExchangeId.ToString());
    }
}