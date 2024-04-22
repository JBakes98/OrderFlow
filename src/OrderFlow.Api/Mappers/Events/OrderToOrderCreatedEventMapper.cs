using OrderFlow.Events;
using OrderFlow.Extensions;
using OrderFlow.Models;

namespace OrderFlow.Mappers.Events;

public class OrderToOrderCreatedEventMapper : IMapper<Order, OrderCreatedEvent>
{
    public OrderCreatedEvent Map(Order source)
    {
        return new OrderCreatedEvent(
            orderId: source.Id,
            instrumentId: source.InstrumentId,
            quantity: source.Quantity,
            price: source.Price,
            createdOn: source.OrderDate);
    }
}