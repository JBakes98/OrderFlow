using Orderflow.Features.Orders.Common;

namespace Orderflow.Extensions;

public static class OrderExtensions
{
    public static Order EntityToDomain(this OrderEntity source)
    {
        return new Order(
            id: source.Id,
            initialQuantity: source.InitialQuantity,
            remainingQuantity: source.RemainingQuantity,
            value: source.Value,
            instrumentId: source.InstrumentId,
            price: source.Price,
            placed: source.Placed,
            updated: source.Updated,
            side: source.Side,
            status: source.Status);
    }

    public static OrderEntity DomainToEntity(this Order source)
    {
        return new OrderEntity(
            id: source.Id,
            initialQuantity: source.InitialQuantity,
            remainingQuantity: source.RemainingQuantity,
            instrumentId: source.InstrumentId,
            price: source.Price,
            value: source.Value,
            placed: source.Placed,
            updated: source.Updated,
            side: source.Side,
            status: source.Status);
    }
}