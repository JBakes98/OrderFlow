namespace Orderflow.Api.Routes.Exchange.Models;

public class PostExchangeRequest
{
    public PostExchangeRequest(string name, string abbreviation, string mic, string region)
    {
        Name = name;
        Abbreviation = abbreviation;
        Mic = mic;
        Region = region;
    }

    public string Name { get; }
    public string Abbreviation { get; }
    public string Mic { get; }
    public string Region { get; }
}