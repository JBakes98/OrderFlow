using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;

namespace OrderFlow.Events;

[JsonDerivedType(typeof(OrderCreatedEvent))]
public class BaseOrderEvent
{
    [DynamoDBHashKey]
    public string EventId { get; } = Guid.NewGuid().ToString();
    public DateTime CreatedOn { get; set; }
    public string EventType { get; set; }
    public string OrderId { get; set; }
}