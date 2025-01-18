using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Mappers.AlphaVantage;
using Orderflow.Services.AlphaVantage.Api.Responses;

namespace Orderflow.Api.Unit.Tests.Mappers.Response.AlphaVantage;

public class GlobalQuoteResponseToGlobalQuoteDomainMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_global_quote_response_to_domain_object(
        GetGlobalQuoteResponse source,
        GlobalQuoteResponseToGlobalQuoteDomainMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(double.Parse(source.High!), result.High);
        Assert.Equal(double.Parse(source.Low!), result.Low);
        Assert.Equal(double.Parse(source.Open!), result.Open);
        Assert.Equal(double.Parse(source.Price!), result.Price);
        Assert.Equal(double.Parse(source.Change!), result.Change);
        Assert.Equal(source.ChangePerc, result.ChangePerc);
        Assert.Equal(int.Parse(source.Volume!), result.Volume);
        Assert.Equal(source.Symbol, result.Symbol);
    }
}