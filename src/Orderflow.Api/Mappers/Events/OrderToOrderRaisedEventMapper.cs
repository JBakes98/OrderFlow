using Orderflow.Domain.Models;
using Orderflow.Events;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Events;

public class OrderToOrderRaisedEventMapper : IMapper<Order, OrderRaisedEvent>
{
    public OrderRaisedEvent Map(Order source)
    {
        return new OrderRaisedEvent(
            source.Id,
            source.InstrumentId,
            source.Quantity,
            source.Price,
            source.Value,
            "Placed");
    }
}