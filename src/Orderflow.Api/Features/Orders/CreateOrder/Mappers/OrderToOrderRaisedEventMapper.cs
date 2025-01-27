using Orderflow.Domain.Models.Enums;
using Orderflow.Features.Common;
using Orderflow.Features.Orders.Common;
using Orderflow.Features.Orders.CreateOrder.Events;

namespace Orderflow.Features.Orders.CreateOrder.Mappers;

public class OrderToOrderRaisedEventMapper : IMapper<Order, OrderRaisedEvent>
{
    public OrderRaisedEvent Map(Order source)
    {
        return source.Side switch
        {
            TradeSide.buy => new BuyOrderRaised(source.Id.ToString(), source.InstrumentId.ToString(),
                source.InitialQuantity, source.Price,
                source.Value, source.Status.ToString()),
            TradeSide.sell => new SellOrderRaised(source.Id.ToString(), source.InstrumentId.ToString(),
                source.InitialQuantity, source.Price,
                source.Value, source.Status.ToString()),
            _ => throw new InvalidOperationException()
        };
    }
}