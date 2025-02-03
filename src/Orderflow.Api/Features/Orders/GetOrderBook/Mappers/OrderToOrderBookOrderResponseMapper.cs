using Orderflow.Common.Mappers;
using Orderflow.Features.Orders.Common.Models;
using Orderflow.Features.Orders.GetOrderBook.Contracts;

namespace Orderflow.Features.Orders.GetOrderBook.Mappers;

public class OrderToOrderBookOrderResponseMapper : IMapper<Order, OrderBooksOrderResponse>
{
    public OrderBooksOrderResponse Map(Order source)
    {
        return new OrderBooksOrderResponse
        {
            Placed = source.Placed,
            Price = source.Price,
            Quantity = source.InitialQuantity,
            Value = source.Value
        };
    }
}