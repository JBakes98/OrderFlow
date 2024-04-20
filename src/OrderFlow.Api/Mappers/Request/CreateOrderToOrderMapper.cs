using OrderFlow.Contracts.Requests;
using OrderFlow.Extensions;
using OrderFlow.Models;

namespace OrderFlow.Mappers.Request;

public class CreateOrderToOrderMapper : IMapper<CreateOrder, Order>
{
    public Order Map(CreateOrder source)
    {
        return new Order();
    }
}