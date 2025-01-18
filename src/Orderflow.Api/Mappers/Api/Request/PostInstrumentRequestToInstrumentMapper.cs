using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Domain.Models;

namespace Orderflow.Mappers.Api.Request;

public class PostInstrumentRequestToInstrumentMapper : IMapper<PostInstrumentRequest, Instrument>
{
    public Instrument Map(PostInstrumentRequest source)
    {
        return new Instrument(
            Guid.NewGuid(),
            source.Ticker,
            source.Name,
            source.Sector,
            Guid.Parse(source.Exchange)
        );
    }
}