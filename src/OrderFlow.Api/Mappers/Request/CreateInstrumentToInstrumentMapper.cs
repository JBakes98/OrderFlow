using OrderFlow.Contracts.Requests;
using OrderFlow.Extensions;
using OrderFlow.Models;

namespace OrderFlow.Mappers.Request;

public class CreateInstrumentToInstrumentMapper : IMapper<CreateInstrument, Instrument>
{
    public Instrument Map(CreateInstrument source)
    {
        return new Instrument(
            name: source.Name,
            sector: source.Sector,
            exchange: source.Exchange
        );
    }
}