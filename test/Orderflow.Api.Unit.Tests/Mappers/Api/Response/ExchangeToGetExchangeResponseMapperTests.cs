using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Domain.Models;
using Orderflow.Mappers.Api.Response;

namespace Orderflow.Api.Unit.Tests.Mappers.Api.Response;

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