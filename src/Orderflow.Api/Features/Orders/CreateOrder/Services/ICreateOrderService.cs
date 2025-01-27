using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Orders.Common;

namespace Orderflow.Features.Orders.CreateOrder.Services;

public interface ICreateOrderService
{
    Task<OneOf<Order, Error>> CreateOrder(Order order);
}