using OrderFlow.Events;
using OrderFlow.Extensions;
using OrderFlow.Models;

namespace OrderFlow.Mappers.Events;

public class OrderToOrderCreatedEventMapper : IMapper<Order, OrderCreatedEvent>
{
    public OrderCreatedEvent Map(Order source)
    {
        return new OrderCreatedEvent{
            OrderId = source.Id,
            InstrumentId = source.InstrumentId,
            Quantity = source.Quantity,
            Price = source.Price,
            CreatedOn = source.OrderDate
        };
    }
}