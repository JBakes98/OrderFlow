using Orderflow.Features.Common.Events;

namespace Orderflow.Features.Trades.CreateTrade.Events;

public class TradeExecutedEvent : IEvent
{
    public TradeExecutedEvent(
        string tradeId, string buyOrderId, string sellOrderId, double price, int quantity, double value,
        DateTime timestamp)
    {
        TradeId = tradeId;
        BuyOrderId = buyOrderId;
        SellOrderId = sellOrderId;
        Price = price;
        Quantity = quantity;
        Value = value;
        Timestamp = timestamp;
    }

    public string TradeId { get; }
    public string BuyOrderId { get; }
    public string SellOrderId { get; }
    public double Price { get; }
    public int Quantity { get; }
    public double Value { get; }
    public DateTime Timestamp { get; }
}