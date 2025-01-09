using Orderflow.Api.Routes.Exchange.Models;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Request;

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