using Orderflow.Extensions;
using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.Common.Repositories;

namespace Orderflow.Features.Orders.Common.Mappers;

public class OrderDomainToOrderDataMapper : IMapper<Order, OrderEntity>
{
    public OrderEntity Map(Order source)
    {
        return source.DomainToEntity();
    }
}