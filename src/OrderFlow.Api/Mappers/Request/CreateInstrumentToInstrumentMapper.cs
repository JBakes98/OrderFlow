using OrderFlow.Contracts.Requests;
using OrderFlow.Domain.Models;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Request;

public class CreateInstrumentToInstrumentMapper : IMapper<CreateInstrument, Instrument>
{
    public Instrument Map(CreateInstrument source)
    {
        return new Instrument(
            ticker: source.Ticker,
            name: source.Name,
            sector: source.Sector,
            exchange: source.Exchange
        );
    }
}