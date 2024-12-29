using Orderflow.Domain.Models;
using Orderflow.Domain.Models.Enums;
using Orderflow.Events;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Events;

public class OrderToOrderRaisedEventMapper : IMapper<Order, OrderRaisedEvent>
{
    public OrderRaisedEvent Map(Order source)
    {
        return source.Type switch
        {
            OrderType.buy => new BuyOrderRaised(source.Id, source.InstrumentId, source.Quantity, source.Price,
                source.Value, source.Status.ToString()),
            OrderType.sell => new SellOrderRaised(source.Id, source.InstrumentId, source.Quantity, source.Price,
                source.Value, source.Status.ToString()),
            _ => throw new InvalidOperationException()
        };
    }
}