using Orderflow.Contracts.Responses.AlphaVantage;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Response.AlphaVantage;

public class GlobalQuoteResponseToGlobalQuoteDomainMapper : IMapper<GlobalQuote, Orderflow.Domain.Models.GlobalQuote>
{
    public Orderflow.Domain.Models.GlobalQuote Map(GlobalQuote source)
    {
        return new Orderflow.Domain.Models.GlobalQuote
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