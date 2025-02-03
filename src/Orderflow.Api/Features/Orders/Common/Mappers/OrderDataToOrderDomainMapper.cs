using Orderflow.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.Common.Repositories;

namespace Orderflow.Features.Orders.Common.Mappers;

public class OrderDataToOrderDomainMapper : IMapper<OrderEntity, Order>
{
    public Order Map(OrderEntity source)
    {
        return new Order(
            id: source.Id,
            initialQuantity: source.InitialQuantity,
            remainingQuantity: source.RemainingQuantity,
            value: source.Value,
            instrumentId: source.InstrumentId,
            price: source.Price,
            placed: source.Placed,
            updated: source.Updated,
            side: source.Side,
            status: source.Status);
    }
}