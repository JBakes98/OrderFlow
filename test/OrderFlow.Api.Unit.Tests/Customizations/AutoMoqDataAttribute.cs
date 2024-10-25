using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;
using OrderFlow.Data.Entities;

namespace OrderFlow.Api.Unit.Tests.Customizations;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute() : base(() =>
    {
        var fixture = new Fixture();

        fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
            .ToList()
            .ForEach(b => fixture.Behaviors.Remove(b));

        fixture.Behaviors.Add(new OmitOnRecursionBehavior());

        fixture
            .Customize(new OrderEntityCustomization())
            .Customize(new InstrumentEntityCustomization())
            .Customize(new GlobalQuoteResponseCustomization());

        // fixture.Customize<OrderEntity>(c => c
        //     .Without(x => x.Instrument));
        // fixture.Customize<InstrumentEntity>(c => c
        //     .Without(x => x.Orders));

        return fixture;
    })
    {
    }
}