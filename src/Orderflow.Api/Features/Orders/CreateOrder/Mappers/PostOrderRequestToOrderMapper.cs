using Orderflow.Common.Mappers;
using Orderflow.Features.Common.Enums;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.CreateOrder.Contracts;

namespace Orderflow.Features.Orders.CreateOrder.Mappers;

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