using AutoFixture;
using Orderflow.Features.Common.Enums;
using Orderflow.Features.Orders.CreateOrder.Contracts;

namespace Orderflow.Api.Unit.Tests.Customizations;

public class PostOrderRequestCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new PostOrderRequest(
            quantity: fixture.Create<int>(),
            instrumentId: Guid.NewGuid().ToString(),
            side: fixture.Create<TradeSide>().ToString(),
            price: fixture.Create<double>())
        );
    }
}