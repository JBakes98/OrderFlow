using OrderFlow.Domain.Models;
using OrderFlow.Events;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Events;

public class OrderToOrderRaisedEventMapper : IMapper<Order, OrderRaisedEvent>
{
    public OrderRaisedEvent Map(Order source)
    {
        return new OrderRaisedEvent(
            orderId: source.Id,
            instrumentId: source.InstrumentId,
            quantity: source.Quantity,
            price: source.Price,
            value: source.Value,
            status: "Placed");
    }
}