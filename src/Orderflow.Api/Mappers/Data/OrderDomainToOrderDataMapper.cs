using Orderflow.Data.Entities;
using Orderflow.Domain.Models;
using Orderflow.Extensions;

namespace Orderflow.Mappers.Data;

public class OrderDomainToOrderDataMapper : IMapper<Order, OrderEntity>
{
    public OrderEntity Map(Order source)
    {
        return new OrderEntity(
            source.Id,
            source.InitialQuantity,
            source.RemainingQuantity,
            source.InstrumentId,
            source.Price,
            source.Value,
            source.Date,
            source.TradeSide,
            source.Status);
    }
}