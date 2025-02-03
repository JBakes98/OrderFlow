using Orderflow.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.Common.Repositories;

namespace Orderflow.Features.Orders.Common.Mappers;

public class OrderDomainToOrderDataMapper : IMapper<Order, OrderEntity>
{
    public OrderEntity Map(Order source)
    {
        return new OrderEntity(
            id: source.Id,
            initialQuantity: source.InitialQuantity,
            remainingQuantity: source.RemainingQuantity,
            instrumentId: source.InstrumentId,
            price: source.Price,
            value: source.Value,
            placed: source.Placed,
            updated: source.Updated,
            side: source.Side,
            status: source.Status);
    }
}