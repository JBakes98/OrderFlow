using AutoFixture;
using Orderflow.Features.Orders.Common;

namespace Orderflow.Api.Unit.Tests.Customizations;

public class OrderEntityCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<OrderEntity>(c => c
            .Without(x => x.Instrument));
    }
}