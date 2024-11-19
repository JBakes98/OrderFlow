namespace OrderFlow.Domain.Models;

public class GlobalQuote
{
    public string Symbol { get; init; }
    public double Open { get; init; }
    public double High { get; init; }
    public double Low { get; init; }
    public double Price { get; init; }
    public int Volume { get; init; }
    public double Change { get; init; }
    public string ChangePerc { get; init; }
}