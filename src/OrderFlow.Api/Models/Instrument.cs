using Amazon.DynamoDBv2.DataModel;

namespace OrderFlow.Models;

[DynamoDBTable("Instrument")]
public class Instrument
{
    public Instrument()
    {
    }
    
    public Instrument(string ticker, string name, string sector, string exchange)
    {
        Ticker = ticker;
        Name = name;
        Sector = sector;
        Exchange = exchange;
    }

    [DynamoDBHashKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Ticker { get; set; }
    public string Name { get; set; }
    public string Sector { get; set; }
    public string Exchange { get; set; }
}