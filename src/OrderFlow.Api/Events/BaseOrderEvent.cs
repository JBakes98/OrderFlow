using System.Text.Json.Serialization;

namespace OrderFlow.Events;

[JsonDerivedType(typeof(OrderCreatedEvent))]
public class BaseOrderEvent
{
    public DateTime CreatedOn { get; set; }
    public string OrderId { get; set; }
}