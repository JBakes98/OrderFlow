using Orderflow.Extensions;
using Orderflow.Features.Common;

namespace Orderflow.Features.Orders.Common.Mappers;

public class OrderDataToOrderDomainMapper : IMapper<OrderEntity, Order>
{
    public Order Map(OrderEntity source)
    {
        return source.EntityToDomain();
    }
}