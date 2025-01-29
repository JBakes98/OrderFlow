using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.GetExchange.Contracts;

namespace Orderflow.Features.Exchanges.GetExchange.Mappers;

public class ExchangeToGetExchangeResponseMapper : IMapper<Exchange, GetExchangeResponse>
{
    public GetExchangeResponse Map(Exchange source)
    {
        return new GetExchangeResponse(
            id: source.Id.ToString(),
            name: source.Name,
            abbreviation: source.Abbreviation,
            mic: source.Mic,
            region: source.Region);
    }
}