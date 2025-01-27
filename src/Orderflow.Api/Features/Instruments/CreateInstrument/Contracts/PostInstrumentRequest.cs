namespace Orderflow.Features.Instruments.CreateInstrument.Contracts;

public class PostInstrumentRequest
{
    public string Ticker { get; }
    public string Name { get; }
    public string Sector { get; }
    public string ExchangeId { get; }

    public PostInstrumentRequest(string ticker, string name, string sector, string exchangeId)
    {
        Ticker = ticker;
        Name = name;
        Sector = sector;
        ExchangeId = exchangeId;
    }
}