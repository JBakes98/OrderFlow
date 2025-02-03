using Orderflow.Common.Mappers;
using Orderflow.Features.Trades.Common.Models;
using Orderflow.Features.Trades.Common.Repositories;

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