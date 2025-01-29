using Orderflow.Features.Common.Mappers;
using Orderflow.Features.Trades.Common.Models;
using Orderflow.Features.Trades.CreateTrade.Events;

namespace Orderflow.Features.Trades.CreateTrade.Mappers;

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