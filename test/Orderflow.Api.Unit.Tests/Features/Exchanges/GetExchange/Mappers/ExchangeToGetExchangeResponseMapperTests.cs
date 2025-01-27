using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Features.Exchanges.Common;
using Orderflow.Features.Exchanges.GetExchange.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Exchanges.GetExchange.Mappers;

public class ExchangeToGetExchangeResponseMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_object_to_get_response(
        Exchange source,
        ExchangeToGetExchangeResponseMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Id.ToString(), result.Id);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Abbreviation, result.Abbreviation);
        Assert.Equal(source.Mic, result.Mic);
        Assert.Equal(source.Region, result.Region);
    }
}