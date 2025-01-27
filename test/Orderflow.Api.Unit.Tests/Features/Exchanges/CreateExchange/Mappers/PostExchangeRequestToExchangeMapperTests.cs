using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Features.Exchanges.CreateExchange.Contracts;
using Orderflow.Features.Exchanges.CreateExchange.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Exchanges.CreateExchange.Mappers;

public class PostExchangeRequestToExchangeMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_create_request_to_domain_object(
        PostExchangeRequest source,
        PostExchangeRequestToExchangeMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Abbreviation, result.Abbreviation);
        Assert.Equal(source.Mic, result.Mic);
        Assert.Equal(source.Region, result.Region);
    }
}