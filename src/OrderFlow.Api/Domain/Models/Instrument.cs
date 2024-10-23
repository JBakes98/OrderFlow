namespace OrderFlow.Domain.Models;

public class Instrument(string ticker, string name, string sector, string exchange)
{
    public string Id { get; } = Guid.NewGuid().ToString();
    public string Ticker { get; } = ticker;
    public string Name { get; } = name;
    public string Sector { get; } = sector;
    public string Exchange { get; } = exchange;
}