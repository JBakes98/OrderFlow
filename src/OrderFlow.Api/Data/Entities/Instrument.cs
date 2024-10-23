namespace OrderFlow.Data.Entities;

public class Instrument(
    string id,
    string ticker,
    string name,
    string sector,
    string exchange)
{
    public string Id { get; } = id;
    public string Ticker { get; } = ticker;
    public string Name { get; } = name;
    public string Sector { get; } = sector;
    public string Exchange { get; } = exchange;
}