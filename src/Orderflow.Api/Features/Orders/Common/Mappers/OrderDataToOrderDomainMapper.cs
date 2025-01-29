using Orderflow.Extensions;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.Common.Repositories;

namespace Orderflow.Features.Orders.Common.Mappers;

public class OrderDataToOrderDomainMapper : IMapper<OrderEntity, Order>
{
    public Order Map(OrderEntity source)
    {
        return source.EntityToDomain();
    }
}