using OrderFlow.Data.Entities;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Data;

public class OrderDomainToOrderDataMapper : IMapper<OrderFlow.Domain.Models.Order, OrderEntity>
{
    public OrderEntity Map(OrderFlow.Domain.Models.Order source)
    {
        return new OrderEntity(
            source.Id,
            source.Quantity,
            source.InstrumentId,
            source.Price,
            source.OrderDate);
    }
}