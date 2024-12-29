using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Response;

public class OrderToGetOrderResponseMapper : IMapper<Order, GetOrderResponse>
{
    public GetOrderResponse Map(Order source)
    {
        return new GetOrderResponse(
            id: source.Id,
            quantity: source.Quantity,
            instrumentId: source.InstrumentId,
            price: source.Price,
            date: source.Date,
            type: source.Type,
            status: source.Status);
    }
}