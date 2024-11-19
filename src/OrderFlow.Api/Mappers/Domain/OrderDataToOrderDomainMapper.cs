using OrderFlow.Data.Entities;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Domain;

public class OrderDataToOrderDomainMapper : IMapper<OrderEntity, OrderFlow.Domain.Models.Order>
{
    public OrderFlow.Domain.Models.Order Map(OrderEntity source)
    {
        return new OrderFlow.Domain.Models.Order(
            source.Id,
            source.Quantity,
            source.InstrumentId,
            source.Price,
            source.Date);
    }
}