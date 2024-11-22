using Orderflow.Contracts.Requests;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Request;

public class CreateOrderToOrderMapper : IMapper<CreateOrder, Order>
{
    public Order Map(CreateOrder source)
    {
        var order = new Order(
            Guid.NewGuid().ToString(),
            source.Quantity,
            source.InstrumentId.ToString(),
            0,
            DateTime.Now.ToUniversalTime());

        order.SetPrice(source.Price);

        return order;
    }
}