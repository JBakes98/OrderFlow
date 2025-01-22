using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Api.Routes.Order.ListOrders.Services;

public interface IListOrdersService
{
    Task<OneOf<IEnumerable<Domain.Models.Order>, Error>> ListOrders();
}