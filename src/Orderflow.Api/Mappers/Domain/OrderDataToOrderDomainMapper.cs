using Orderflow.Data.Entities;
using Orderflow.Domain.Models;

namespace Orderflow.Mappers.Domain;

public class OrderDataToOrderDomainMapper : IMapper<OrderEntity, Order>
{
    public Order Map(OrderEntity source)
    {
        return new Order(
            source.Id,
            source.Quantity,
            source.InstrumentId,
            source.Price,
            source.Date,
            source.Type,
            source.Status);
    }
}