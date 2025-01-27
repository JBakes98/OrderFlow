using Orderflow.Features.Common;

namespace Orderflow.Features.Exchanges.Common.Mappers;

public class ExchangeDomainToExchangeDataMapper : IMapper<Exchange, ExchangeEntity>
{
    public ExchangeEntity Map(Exchange source)
    {
        return new ExchangeEntity(
            source.Id,
            source.Name,
            source.Abbreviation,
            source.Mic,
            source.Region);
    }
}