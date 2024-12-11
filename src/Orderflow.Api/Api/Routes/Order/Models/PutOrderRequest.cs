using Orderflow.Domain.Models.Enums;

namespace Orderflow.Api.Routes.Order.Models;

public class PutOrderRequest
{
    public PutOrderRequest(
        string id,
        OrderStatus status)
    {
        Id = id;
        Status = status;
    }

    public string Id { get; private set; }
    public OrderStatus Status { get; }

    public void SetOrderId(string id)
    {
        Id = id;
    }
}