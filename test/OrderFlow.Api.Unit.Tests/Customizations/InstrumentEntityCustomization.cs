using AutoFixture;
using OrderFlow.Data.Entities;

namespace OrderFlow.Api.Unit.Tests.Customizations;

public class InstrumentEntityCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<InstrumentEntity>(c => c
            .Without(x => x.Orders));
    }
}