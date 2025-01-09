using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models;

namespace Orderflow.Mappers.Response;

public class OrderToGetOrderResponseMapper : IMapper<Order, GetOrderResponse>
{
    public GetOrderResponse Map(Order source)
    {
        return new GetOrderResponse(
            id: source.Id.ToString(),
            quantity: source.InitialQuantity,
            instrumentId: source.InstrumentId.ToString(),
            price: source.Price,
            value: source.Value,
            placed: source.Placed,
            updated: source.Updated,
            type: source.Side,
            status: source.Status);
    }
}