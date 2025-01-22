using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Domain.Models;

namespace Orderflow.Mappers.Api.Response;

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