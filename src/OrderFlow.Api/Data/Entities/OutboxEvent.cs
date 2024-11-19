using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;

namespace OrderFlow.Data.Entities;

public class OutboxEvent
{
    [Key]
    [Column(TypeName = "varchar")]
    [MaxLength(36)]
    public string Id { get; private set; }

    [Column(TypeName = "varchar")]
    [MaxLength(36)]
    public string StreamId { get; private set; }

    [Column(TypeName = "varchar")]
    [MaxLength(50)]
    public string EventType { get; private set; }

    [Column(TypeName = "timestamp with time zone")]
    public DateTime Timestamp { get; private set; }

    [Column(TypeName = "jsonb")] public string Payload { get; private set; }

    public OutboxEvent()
    {
    }

    public OutboxEvent(
        string id,
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