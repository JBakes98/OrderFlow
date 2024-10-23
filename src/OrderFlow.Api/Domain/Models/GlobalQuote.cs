namespace OrderFlow.Domain.Models;

public class GlobalQuote
{
    public string Symbol { get; set; }
    public double Open { get; set; }
    public double High { get; set; }
    public double Low { get; set; }
    public double Price { get; set; }
    public int Volume { get; set; }
    public double Change { get; set; }
    public string ChangePerc { get; set; }
}