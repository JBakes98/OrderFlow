using OrderFlow.Contracts.Requests;
using OrderFlow.Domain.Models;
using OrderFlow.Extensions;

namespace OrderFlow.Mappers.Request;

public class CreateOrderToOrderMapper : IMapper<CreateOrder, Order>
{
    public Order Map(CreateOrder source)
    {
        return new Order(
            instrumentId: source.InstrumentId.ToString(),
            quantity: source.Quantity);
    }
}