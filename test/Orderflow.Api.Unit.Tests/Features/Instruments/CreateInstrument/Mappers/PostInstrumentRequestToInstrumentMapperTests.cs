using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Features.Instruments.CreateInstrument.Contracts;
using Orderflow.Features.Instruments.CreateInstrument.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Instruments.CreateInstrument.Mappers;

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
        Assert.Equal(Guid.Parse(source.ExchangeId), result.ExchangeId);
        Assert.Equal(source.Sector, result.Sector);
    }
}