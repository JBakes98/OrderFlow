using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Contracts.Requests;
using OrderFlow.Mappers.Request;

namespace OrderFlow.Api.Unit.Tests.Mappers.Request;

public class CreateInstrumentToInstrumentMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_create_request_to_domain_object(
        CreateInstrument source,
        CreateInstrumentToInstrumentMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Exchange, result.Exchange);
        Assert.Equal(source.Sector, result.Sector);

        Assert.NotNull(result.Id);
    }
}