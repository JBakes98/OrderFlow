using Orderflow.Data.Entities;
using Orderflow.Domain.Models;
using Orderflow.Extensions;
using Orderflow.Features.Exchanges.Common;

namespace Orderflow.Mappers.Domain;

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