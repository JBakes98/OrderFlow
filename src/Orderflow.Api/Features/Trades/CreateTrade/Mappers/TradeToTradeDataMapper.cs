using Orderflow.Features.Common;
using Orderflow.Features.Trades.Common;

namespace Orderflow.Features.Trades.CreateTrade.Mappers;

public class TradeToTradeDataMapper : IMapper<Trade, TradeEntity>
{
    public TradeEntity Map(Trade source)
    {
        return new TradeEntity(
            id: source.Id,
            buyOrderId: source.BuyOrderId,
            sellOrderId: source.SellOrderId,
            price: source.Price,
            quantity: source.Quantity,
            value: source.Value,
            timestamp: source.Timestamp);
    }
}