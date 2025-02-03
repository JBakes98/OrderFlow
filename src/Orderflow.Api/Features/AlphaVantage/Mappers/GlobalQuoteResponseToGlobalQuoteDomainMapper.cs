using Orderflow.Common.Mappers;
using Orderflow.Features.AlphaVantage.Contracts;
using Orderflow.Features.AlphaVantage.Models;

namespace Orderflow.Features.AlphaVantage.Mappers;

public class
    GlobalQuoteResponseToGlobalQuoteDomainMapper : IMapper<GetGlobalQuoteResponse, GlobalQuote>
{
    public GlobalQuote Map(GetGlobalQuoteResponse source)
    {
        return new GlobalQuote
        {
            Symbol = source.Symbol,
            Change = double.Parse(source.Change),
            ChangePerc = source.ChangePerc,
            High = double.Parse(source.High),
            Low = double.Parse(source.Low),
            Open = double.Parse(source.Open),
            Price = double.Parse(source.Price),
            Volume = int.Parse(source.Volume)
        };
    }
}