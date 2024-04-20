namespace OrderFlow.Contracts.Requests;

public class CreateInstrument
{
    public string Name { get; set; }
    public string Sector { get; set; }
    public string Exchange { get; set; }

    public CreateInstrument(string name, string sector, string exchange)
    {
        Name = name;
        Sector = sector;
        Exchange = exchange;
    }
}