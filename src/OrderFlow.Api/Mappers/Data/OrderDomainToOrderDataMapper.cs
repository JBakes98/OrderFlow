using OrderFlow.Data.Entities;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Data;

public class OrderDomainToOrderDataMapper : IMapper<OrderFlow.Domain.Models.Order, Order>
{
    public Order Map(OrderFlow.Domain.Models.Order source)
    {
        return new Order(
            source.Id,
            source.Quantity,
            source.InstrumentId,
            source.Price,
            source.OrderDate);
    }
}