using Amazon.DynamoDBv2.DataModel;

namespace OrderFlow.Models;

[DynamoDBTable("Instrument")]
public class Instrument
{   
    [DynamoDBHashKey]
    public string Id { get; set; } = Guid.NewGuid().ToString();
    public string Name { get; set; }
    public string Sector { get; set; }
    public string Exchange { get; set; }

    public Instrument()
    {
    }

    public Instrument(string name, string sector, string exchange)
    {
        Name = name;
        Sector = sector;
        Exchange = exchange;
    }
}