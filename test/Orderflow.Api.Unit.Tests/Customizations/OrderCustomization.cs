using AutoFixture;
using Orderflow.Domain.Models.Enums;
using Orderflow.Features.Orders.Common;

namespace Orderflow.Api.Unit.Tests.Customizations;

public class OrderCustomization : ICustomization
{
    private readonly TradeSide _side;

    public OrderCustomization(TradeSide side = TradeSide.buy)
    {
        _side = side;
    }

    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new Order(
            initialQuantity: fixture.Create<int>(),
            instrumentId: Guid.NewGuid(),
            price: fixture.Create<double>(),
            side: _side
        ));
    }
}