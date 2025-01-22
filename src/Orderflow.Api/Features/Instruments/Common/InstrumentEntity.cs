using System.ComponentModel.DataAnnotations;

namespace Orderflow.Data.Entities;

public class InstrumentEntity
{
    public InstrumentEntity()
    {
    }

    public InstrumentEntity(
        Guid id,
        string ticker,
        string name,
        string sector,
        Guid exchangeId)
    {
        Id = id;
        Ticker = ticker;
        Name = name;
        Sector = sector;
        ExchangeId = exchangeId;
    }

    [Key] public Guid Id { get; private set; }
    [MaxLength(5)] [Required] public string Ticker { get; private set; }
    [MaxLength(256)] public string Name { get; private set; }
    [MaxLength(256)] public string Sector { get; private set; }
    [Required] public Guid ExchangeId { get; private set; }
    public virtual ExchangeEntity Exchange { get; init; } = null!;
    public ICollection<OrderEntity>? Orders { get; set; }
}