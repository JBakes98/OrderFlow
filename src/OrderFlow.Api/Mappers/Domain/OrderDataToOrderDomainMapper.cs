using OrderFlow.Data.Entities;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Domain;

public class OrderDataToOrderDomainMapper : IMapper<Order, OrderFlow.Domain.Models.Order>
{
    public OrderFlow.Domain.Models.Order Map(Order source)
    {
        return new OrderFlow.Domain.Models.Order(
            source.Id,
            source.Quantity,
            source.InstrumentId,
            source.Price,
            source.OrderDate);
    }
}