using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace Orderflow.Api.Unit.Tests.Customizations;

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
            .Customize(new GlobalQuoteResponseCustomization())
            .Customize(new PostInstrumentRequestCustomization())
            .Customize(new PostOrderRequestCustomization())
            .Customize(new AutoMoqCustomization());

        return fixture;
    })
    {
    }
}