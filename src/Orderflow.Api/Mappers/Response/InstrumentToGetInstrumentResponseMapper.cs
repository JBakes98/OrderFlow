using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Response;

public class InstrumentToGetInstrumentResponseMapper : IMapper<Instrument, GetInstrumentResponse>
{
    public GetInstrumentResponse Map(Instrument source)
    {
        return new GetInstrumentResponse(
            id: source.Id,
            ticker: source.Ticker,
            name: source.Name,
            sector: source.Sector,
            exchange: source.Exchange);
    }
}