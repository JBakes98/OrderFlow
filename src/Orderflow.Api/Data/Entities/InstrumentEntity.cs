using System.ComponentModel.DataAnnotations;

namespace Orderflow.Data.Entities;

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

    [Key][MaxLength(36)] public string Id { get; private set; } = null!;
    [MaxLength(5)] public string Ticker { get; private set; } = null!;
    [MaxLength(256)] public string Name { get; private set; } = null!;
    [MaxLength(256)] public string Sector { get; private set; } = null!;
    [MaxLength(256)] public string Exchange { get; private set; } = null!;
    public ICollection<OrderEntity>? Orders { get; set; }
}
