using Orderflow.Common.Mappers;
using Orderflow.Features.Instruments.Common.Models;
using Orderflow.Features.Instruments.CreateInstrument.Contracts;

namespace Orderflow.Features.Instruments.CreateInstrument.Mappers;

public class PostInstrumentRequestToInstrumentMapper : IMapper<PostInstrumentRequest, Instrument>
{
    public Instrument Map(PostInstrumentRequest source)
    {
        return new Instrument(
            Guid.NewGuid(),
            source.Ticker,
            source.Name,
            source.Sector,
            Guid.Parse(source.ExchangeId)
        );
    }
}