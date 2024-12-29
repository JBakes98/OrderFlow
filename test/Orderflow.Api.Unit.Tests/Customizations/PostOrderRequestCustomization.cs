using AutoFixture;
using Orderflow.Api.Routes.Order.Models;
using Orderflow.Domain.Models.Enums;

namespace Orderflow.Api.Unit.Tests.Customizations;

public class PostOrderRequestCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new PostOrderRequest(
            quantity: fixture.Create<int>(),
            instrumentId: Guid.NewGuid().ToString(),
            type: fixture.Create<OrderType>().ToString())
        );
    }
}