using Orderflow.Domain.Commands;
using Orderflow.Events.Order;

namespace Orderflow.Mappers.Events;

public class OrderUpdateToOrderUpdateEventMapper : IMapper<OrderUpdateCommand, OrderUpdateEvent>
{
    public OrderUpdateEvent Map(OrderUpdateCommand source)
    {
        return new OrderUpdateEvent(
            orderId: source.Id,
            status: source.Status.ToString()
        );
    }
}