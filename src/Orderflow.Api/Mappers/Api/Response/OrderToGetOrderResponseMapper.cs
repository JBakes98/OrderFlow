using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models;

namespace Orderflow.Mappers.Api.Response;

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