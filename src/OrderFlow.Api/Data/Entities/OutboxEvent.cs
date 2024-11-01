using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Newtonsoft.Json;

namespace OrderFlow.Data.Entities;

[Table("OutboxEvents")]
public class OutboxEvent
{
    [Key] public Guid Id { get; }
    public string StreamId { get; }
    public string EventType { get; }
    public DateTime Timestamp { get; }

    [Column(TypeName = "jsonb")] public string Payload { get; private set; }

    public OutboxEvent(
        Guid id,
        string streamId,
        string eventType,
        DateTime timestamp)
    {
        Id = id;
        StreamId = streamId;
        EventType = eventType;
        Timestamp = timestamp;
    }

    public void SetPayload<T>(T payload)
    {
        Payload = JsonConvert.SerializeObject(payload);
    }

    public T? GetPayload<T>()
    {
        return JsonConvert.DeserializeObject<T>(Payload);
    }
}