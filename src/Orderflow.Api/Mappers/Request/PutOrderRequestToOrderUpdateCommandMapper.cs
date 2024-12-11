using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Commands;

namespace Orderflow.Mappers.Request;

public class PutOrderRequestToOrderUpdateCommandMapper : IMapper<PutOrderRequest, OrderUpdateCommand>
{
    public OrderUpdateCommand Map(PutOrderRequest source)
    {
        return new OrderUpdateCommand(
            id: source.Id,
            status: source.Status
        );
    }
}