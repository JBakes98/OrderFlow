using AutoFixture;
using Orderflow.Features.Instruments.Common.Repositories;

namespace Orderflow.Api.Unit.Tests.Customizations;

public class InstrumentEntityCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Customize<InstrumentEntity>(c => c
            .Without(x => x.Orders));
    }
}