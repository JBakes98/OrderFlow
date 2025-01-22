namespace Orderflow.Domain.Models;

public class Exchange
{
    public Exchange(Guid id, string name, string abbreviation, string mic, string region)
    {
        Id = id;
        Name = name;
        Abbreviation = abbreviation;
        Mic = mic;
        Region = region;
    }

    public Exchange(string name, string abbreviation, string mic, string region)
    {
        Id = Guid.NewGuid();
        Name = name;
        Abbreviation = abbreviation;
        Mic = mic;
        Region = region;
    }

    public Guid Id { get; }
    public string Name { get; }
    public string Abbreviation { get; }
    public string Mic { get; }
    public string Region { get; }
}