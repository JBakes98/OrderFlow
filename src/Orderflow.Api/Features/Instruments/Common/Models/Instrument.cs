namespace Orderflow.Features.Instruments.Common.Models;

public class Instrument
{
    public Instrument(
        Guid id,
        string ticker,
        string name,
        string sector,
        Guid exchange)
    {
        Id = id;
        Ticker = ticker;
        Name = name;
        Sector = sector;
        ExchangeId = exchange;
    }

    public Guid Id { get; }
    public string Ticker { get; }
    public string Name { get; }
    public string Sector { get; }
    public Guid ExchangeId { get; }
}