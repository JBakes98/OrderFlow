using Orderflow.Api.Routes.Exchange.Models;
using Orderflow.Domain.Models;
using Orderflow.Features.Exchanges.Common;

namespace Orderflow.Mappers.Api.Response;

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