namespace OrderFlow.Events;

public class InstrumentCreatedEvent : IEvent
{
    public InstrumentCreatedEvent(string instrumentId, string ticker, string name, string exchange, string sector)
    {
        InstrumentId = instrumentId;
        Ticker = ticker;
        Name = name;
        Exchange = exchange;
        Sector = sector;
    }

    public string InstrumentId { get; }
    public string Ticker { get; }
    public string Name { get; }
    public string Exchange { get; }
    public string Sector { get; }
}