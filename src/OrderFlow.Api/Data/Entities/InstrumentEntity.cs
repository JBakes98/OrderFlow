using System.ComponentModel.DataAnnotations;

namespace OrderFlow.Data.Entities;

public class InstrumentEntity
{
    public InstrumentEntity()
    {
    }

    public InstrumentEntity(
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

    [Key] public string Id { get; private set; }
    public string Ticker { get; private set; }
    public string Name { get; private set; }
    public string Sector { get; private set; }
    public string Exchange { get; private set; }
}