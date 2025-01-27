using System.ComponentModel.DataAnnotations;
using Orderflow.Features.Instruments.Common;

namespace Orderflow.Features.Exchanges.Common;

public class ExchangeEntity
{
    public ExchangeEntity()
    {
    }

    public ExchangeEntity(
        Guid id, string name, string abbreviation, string mic, string region)
    {
        Id = id;
        Name = name;
        Abbreviation = abbreviation;
        Mic = mic;
        Region = region;
    }

    [Key] public Guid Id { get; private set; }
    [MaxLength(256)] public string Name { get; private set; }
    [MaxLength(50)] public string Abbreviation { get; private set; }
    [MaxLength(4)] public string Mic { get; private set; }
    [MaxLength(256)] public string Region { get; private set; }
    public ICollection<InstrumentEntity>? Instruments { get; set; }
}