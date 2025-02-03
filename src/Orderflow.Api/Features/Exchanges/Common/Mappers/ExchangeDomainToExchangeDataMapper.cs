using Orderflow.Common.Mappers;
using Orderflow.Features.Exchanges.Common.Models;
using Orderflow.Features.Exchanges.Common.Repositories;

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