namespace Orderflow.Events.Exchange;

public class ExchangeCreatedEvent : IEvent
{
    public ExchangeCreatedEvent(string exchangeId, string name, string abbreviation, string mic, string region)
    {
        ExchangeId = exchangeId;
        Name = name;
        Abbreviation = abbreviation;
        Mic = mic;
        Region = region;
    }

    public string ExchangeId { get; }
    public string Name { get; }
    public string Abbreviation { get; }
    public string Mic { get; }
    public string Region { get; }
}