namespace OrderFlow.Data.Entities;

public class Instrument
{
    public Instrument(string ticker, string name, string sector, string exchange)
    {
        Ticker = ticker;
        Name = name;
        Sector = sector;
        Exchange = exchange;
    }

    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Ticker { get; set; }
    public string Name { get; set; }
    public string Sector { get; set; }
    public string Exchange { get; set; }
}