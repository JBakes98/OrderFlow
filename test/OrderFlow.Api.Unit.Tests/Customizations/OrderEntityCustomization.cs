using AutoFixture;
using Orderflow.Data.Entities;

namespace Orderflow.Api.Unit.Tests.Customizations;

public class OrderEntityCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<OrderEntity>(c => c
            .Without(x => x.Instrument));
    }
}