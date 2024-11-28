namespace Orderflow.Api.Routes.Instrument.Models;

public class PostInstrumentRequest
{
    public string Ticker { get; set; }
    public string Name { get; set; }
    public string Sector { get; set; }
    public string Exchange { get; set; }

    public PostInstrumentRequest(string ticker, string name, string sector, string exchange)
    {
        Ticker = ticker;
        Name = name;
        Sector = sector;
        Exchange = exchange;
    }
}