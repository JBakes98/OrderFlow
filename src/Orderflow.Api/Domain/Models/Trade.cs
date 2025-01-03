namespace Orderflow.Domain.Models;

public class Trade
{
    public Trade(TradeInfo bidTrade, TradeInfo askTrade)
    {
        BidTrade = bidTrade;
        AskTrade = askTrade;
    }

    public Guid Id { get; } = new Guid();
    public TradeInfo BidTrade { get; set; }
    public TradeInfo AskTrade { get; set; }
}