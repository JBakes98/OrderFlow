namespace Orderflow.Contracts.Requests;

public class CreateInstrument
{
    public string Ticker { get; set; }
    public string Name { get; set; }
    public string Sector { get; set; }
    public string Exchange { get; set; }

    public CreateInstrument(string ticker, string name, string sector, string exchange)
    {
        Ticker = ticker;
        Name = name;
        Sector = sector;
        Exchange = exchange;
    }
}