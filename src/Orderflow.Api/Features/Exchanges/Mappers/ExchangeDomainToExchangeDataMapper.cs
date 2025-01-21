using Orderflow.Data.Entities;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Data;

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