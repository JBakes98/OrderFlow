using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Events.Exchange;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Events;

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