namespace Orderflow.Domain.Models;

public class Instrument
{
    public Instrument(
        string id,
        string ticker,
        string name,
        string sector,
        string exchange)
    {
        Id = id;
        Ticker = ticker;
        Name = name;
        Sector = sector;
        Exchange = exchange;
    }

    public string Id { get; }
    public string Ticker { get; }
    public string Name { get; }
    public string Sector { get; }
    public string Exchange { get; }
}