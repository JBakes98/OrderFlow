using Orderflow.Domain.Models;
using Orderflow.Events.Trade;

namespace Orderflow.Mappers.Events;

public class TradeToTradeExecutedEventMapper : IMapper<Trade, TradeExecutedEvent>
{
    public TradeExecutedEvent Map(Trade source)
    {
        return new TradeExecutedEvent(
            tradeId: source.Id.ToString(),
            buyOrderId: source.BuyOrderId.ToString(),
            sellOrderId: source.SellOrderId.ToString(),
            price: source.Price,
            quantity: source.Quantity,
            value: source.Value,
            timestamp: source.Timestamp
        );
    }
}