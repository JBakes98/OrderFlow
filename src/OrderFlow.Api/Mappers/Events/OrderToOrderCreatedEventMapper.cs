using OrderFlow.Domain.Models;
using OrderFlow.Events;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Events;

public class OrderToOrderCreatedEventMapper : IMapper<Order, OrderCreatedEvent>
{
    public OrderCreatedEvent Map(Order source)
    {
        return new OrderCreatedEvent
        {
            OrderId = source.Id,
            InstrumentId = source.InstrumentId,
            Quantity = source.Quantity,
            Price = source.Price,
            CreatedOn = source.OrderDate
        };
    }
}