using OneOf;
using Orderflow.Domain.Models;

namespace Orderflow.Api.Routes.Order.GetOrder.Services;

public interface IGetOrderService
{
    Task<OneOf<Domain.Models.Order, Error>> GetOrder(string id);
}