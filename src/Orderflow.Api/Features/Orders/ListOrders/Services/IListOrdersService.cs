using OneOf;
using Orderflow.Features.Common;
using Orderflow.Features.Orders.Common;

namespace Orderflow.Features.Orders.ListOrders.Services;

public interface IListOrdersService
{
    Task<OneOf<IEnumerable<Order>, Error>> ListOrders();
}