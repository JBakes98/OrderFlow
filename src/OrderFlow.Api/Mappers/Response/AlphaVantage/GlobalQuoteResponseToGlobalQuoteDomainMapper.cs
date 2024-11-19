using OrderFlow.Contracts.Responses.AlphaVantage;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Response.AlphaVantage;

public class GlobalQuoteResponseToGlobalQuoteDomainMapper : IMapper<GlobalQuote, OrderFlow.Domain.Models.GlobalQuote>
{
    public OrderFlow.Domain.Models.GlobalQuote Map(GlobalQuote source)
    {
        return new OrderFlow.Domain.Models.GlobalQuote
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