using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models;

namespace Orderflow.Mappers.Request;

public class PostOrderRequestToOrderMapper : IMapper<PostOrderRequest, Order>
{
    public Order Map(PostOrderRequest source)
    {
        var order = new Order(
            Guid.NewGuid().ToString(),
            source.Quantity,
            source.InstrumentId,
            0,
            DateTime.Now.ToUniversalTime(),
            source.Type);

        return order;
    }
}