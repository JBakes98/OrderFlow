using Orderflow.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.GetOrder.Contracts;

namespace Orderflow.Features.Orders.GetOrder.Mappers;

public class OrderToGetOrderResponseMapper : IMapper<Order, GetOrderResponse>
{
    public GetOrderResponse Map(Order source)
    {
        return new GetOrderResponse(
            id: source.Id.ToString(),
            quantity: source.InitialQuantity,
            remainingQuantity: source.RemainingQuantity,
            instrumentId: source.InstrumentId.ToString(),
            price: source.Price,
            value: source.Value,
            placed: source.Placed,
            updated: source.Updated,
            side: source.Side,
            status: source.Status);
    }
}