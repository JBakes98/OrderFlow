using Orderflow.Extensions;
using Orderflow.Features.Common;

namespace Orderflow.Features.Orders.Common.Mappers;

public class OrderDomainToOrderDataMapper : IMapper<Order, OrderEntity>
{
    public OrderEntity Map(Order source)
    {
        return source.DomainToEntity();
    }
}