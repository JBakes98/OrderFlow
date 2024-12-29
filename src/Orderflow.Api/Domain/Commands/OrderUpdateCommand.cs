using Orderflow.Domain.Models.Enums;

namespace Orderflow.Domain.Commands;

public class OrderUpdateCommand
{
    public OrderUpdateCommand(string id,
        OrderStatus status)
    {
        Id = id;
        Status = status;
    }

    public string Id { get; }
    public OrderStatus Status { get; }
}