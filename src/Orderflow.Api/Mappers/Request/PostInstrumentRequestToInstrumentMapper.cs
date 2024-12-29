using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Request;

public class PostInstrumentRequestToInstrumentMapper : IMapper<PostInstrumentRequest, Instrument>
{
    public Instrument Map(PostInstrumentRequest source)
    {
        return new Instrument(
            Guid.NewGuid().ToString(),
            source.Ticker,
            source.Name,
            source.Sector,
            source.Exchange
        );
    }
}