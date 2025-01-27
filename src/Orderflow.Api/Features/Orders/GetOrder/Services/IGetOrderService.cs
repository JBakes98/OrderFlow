using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Orders.Common;

namespace Orderflow.Features.Orders.GetOrder.Services;

public interface IGetOrderService
{
    Task<OneOf<Order, Error>> GetOrder(string id);
}