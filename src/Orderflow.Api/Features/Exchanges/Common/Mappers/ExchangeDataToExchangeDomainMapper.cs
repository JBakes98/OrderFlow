using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.Common.Repositories;

namespace Orderflow.Features.Exchanges.Common.Mappers;

public class ExchangeDataToExchangeDomainMapper : IMapper<ExchangeEntity, Exchange>
{
    public Exchange Map(ExchangeEntity source)
    {
        return new Exchange(
            source.Id,
            source.Name,
            source.Abbreviation,
            source.Mic,
            source.Region);
    }
}