using System.ComponentModel.DataAnnotations;

namespace OrderFlow.Events;

public class Event
{
    [Key]    
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string @event { get; set; }
    public DateTime Timestamp { get; } = DateTime.Now.ToUniversalTime();
    public string StreamId { get; set; }
    public string EventType { get; set; }
}