using Orderflow.Features.Common;
using Orderflow.Features.Exchanges.Common;
using Orderflow.Features.Exchanges.CreateExchange.Events;

namespace Orderflow.Features.Exchanges.CreateExchange.Mappers;

public class ExchangeToExchangeCreatedEventMapper : IMapper<Exchange, ExchangeCreatedEvent>
{
    public ExchangeCreatedEvent Map(Exchange source)
    {
        return new ExchangeCreatedEvent(
            source.Id.ToString(),
            source.Name,
            source.Abbreviation,
            source.Mic,
            source.Region);
    }
}