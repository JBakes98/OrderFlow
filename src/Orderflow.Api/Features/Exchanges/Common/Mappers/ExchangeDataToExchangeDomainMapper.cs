using Orderflow.Features.Common;

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