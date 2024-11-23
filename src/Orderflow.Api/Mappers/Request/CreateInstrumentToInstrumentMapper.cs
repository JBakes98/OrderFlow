using Orderflow.Contracts.Requests;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Request;

public class CreateInstrumentToInstrumentMapper : IMapper<CreateInstrument, Instrument>
{
    public Instrument Map(CreateInstrument source)
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