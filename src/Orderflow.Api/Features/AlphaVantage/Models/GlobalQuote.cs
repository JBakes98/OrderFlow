namespace Orderflow.Domain.Models;

public class GlobalQuote
{
    public required string Symbol { get; init; }
    public double Open { get; init; }
    public double High { get; init; }
    public double Low { get; init; }
    public double Price { get; init; }
    public int Volume { get; init; }
    public double Change { get; init; }
    public required string ChangePerc { get; init; }
}