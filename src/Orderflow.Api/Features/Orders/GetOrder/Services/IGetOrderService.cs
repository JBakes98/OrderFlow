using OneOf;
using Orderflow.Features.Common.Models;
using Orderflow.Features.Orders.Common.Models;

namespace Orderflow.Features.Orders.GetOrder.Services;

public interface IGetOrderService
{
    Task<OneOf<Order, Error>> GetOrder(string id);
}