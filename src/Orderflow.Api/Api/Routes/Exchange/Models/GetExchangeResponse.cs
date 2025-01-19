namespace Orderflow.Api.Routes.Exchange.Models;

public class GetExchangeResponse
{
    public string Id { get; }
    public string Name { get; }
    public string Abbreviation { get; }
    public string Mic { get; }
    public string Region { get; }

    public GetExchangeResponse(string id, string name, string abbreviation, string mic, string region)
    {
        Id = id;
        Name = name;
        Abbreviation = abbreviation;
        Mic = mic;
        Region = region;
    }
}