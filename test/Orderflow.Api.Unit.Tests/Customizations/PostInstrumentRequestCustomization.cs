using AutoFixture;
using Orderflow.Api.Routes.Instrument.Models;

namespace Orderflow.Api.Unit.Tests.Customizations;

public class PostInstrumentRequestCustomization : ICustomization
{
    public void Customize(IFixture fixture)
    {
        fixture.Register(() => new PostInstrumentRequest(
            ticker: fixture.Create<string>().Substring(0, 4),
            name: fixture.Create<string>(),
            sector: fixture.Create<string>(),
            Guid.NewGuid().ToString())
        );
    }
}