using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models;
using Orderflow.Domain.Models.Enums;

namespace Orderflow.Mappers.Api.Request;

public class PostOrderRequestToOrderMapper : IMapper<PostOrderRequest, Order>
{
    public Order Map(PostOrderRequest source)
    {
        var order = new Order(
            initialQuantity: source.Quantity,
            instrumentId: Guid.Parse(source.InstrumentId),
            price: source.Price,
            side: Enum.Parse<TradeSide>(source.Side));

        return order;
    }
}