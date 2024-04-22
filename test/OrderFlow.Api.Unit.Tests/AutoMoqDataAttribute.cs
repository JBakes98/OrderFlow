using AutoFixture;
using AutoFixture.AutoMoq;
using AutoFixture.Xunit2;

namespace OrderFlow.Api.Unit.Tests;

public class AutoMoqDataAttribute : AutoDataAttribute
{
    public AutoMoqDataAttribute()
        : base(new Fixture().Customize(new AutoMoqCustomization()))
    {
    }
}