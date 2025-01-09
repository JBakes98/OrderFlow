using Orderflow.Api.Routes.Instrument.Models;
using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Mappers.Request;

namespace Orderflow.Api.Unit.Tests.Mappers.Request;

public class PostInstrumentRequestToInstrumentMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_create_request_to_domain_object(
        PostInstrumentRequest source,
        PostInstrumentRequestToInstrumentMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(Guid.Parse(source.Exchange), result.ExchangeId);
        Assert.Equal(source.Sector, result.Sector);

        Assert.NotNull(result.Id);
    }
}