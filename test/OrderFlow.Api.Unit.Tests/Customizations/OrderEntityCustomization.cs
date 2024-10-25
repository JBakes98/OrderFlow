using AutoFixture;
using OrderFlow.Data.Entities;

namespace OrderFlow.Api.Unit.Tests.Customizations;

public class OrderEntityCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<OrderEntity>(c => c
            .Without(x => x.Instrument));
    }
}