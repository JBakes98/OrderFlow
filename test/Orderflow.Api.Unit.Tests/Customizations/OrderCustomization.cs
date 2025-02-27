using AutoFixture;
using Orderflow.Features.Common.Enums;
using Orderflow.Features.Orders.Common.Models;

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