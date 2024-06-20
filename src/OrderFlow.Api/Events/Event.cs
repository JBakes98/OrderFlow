using System.Text.Json.Serialization;
using Amazon.DynamoDBv2.DataModel;

namespace OrderFlow.Events;

[DynamoDBTable("Events")]
public class Event
{
    public string Id { get; } = Guid.NewGuid().ToString();
    [JsonPropertyName("Event")] 
    public string @event { get; set; }
    public DateTime Timestamp { get; } = DateTime.Now;
    public string StreamId { get; set; }
    public string EventType { get; set; }
    public long _deletionDate { get; } = ((DateTimeOffset)DateTime.Now.AddDays(7)).ToUnixTimeSeconds();
}