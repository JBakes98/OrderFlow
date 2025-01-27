using Orderflow.Features.Common;
using Orderflow.Features.Exchanges.Common;
using Orderflow.Features.Exchanges.CreateExchange.Contracts;

namespace Orderflow.Features.Exchanges.CreateExchange.Mappers;

public class PostExchangeRequestToExchangeMapper : IMapper<PostExchangeRequest, Exchange>
{
    public Exchange Map(PostExchangeRequest source)
    {
        return new Exchange(
            source.Name,
            source.Abbreviation,
            source.Mic,
            source.Region
        );
    }
}