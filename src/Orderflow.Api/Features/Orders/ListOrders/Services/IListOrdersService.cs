using OneOf;
using Orderflow.Common.Models;
using Orderflow.Features.Orders.Common.Models;

namespace Orderflow.Features.Orders.ListOrders.Services;

public interface IListOrdersService
{
    Task<OneOf<IEnumerable<Order>, Error>> ListOrders();
}