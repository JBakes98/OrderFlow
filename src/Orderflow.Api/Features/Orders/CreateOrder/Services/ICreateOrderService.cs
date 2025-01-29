using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Orders.Common.Models;

namespace Orderflow.Features.Orders.CreateOrder.Services;

public interface ICreateOrderService
{
    Task<OneOf<Order, Error>> CreateOrder(Order order);
}