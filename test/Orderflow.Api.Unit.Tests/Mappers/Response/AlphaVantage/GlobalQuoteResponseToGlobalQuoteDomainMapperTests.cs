using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Contracts.Responses.AlphaVantage;
using Orderflow.Mappers.Response.AlphaVantage;

namespace Orderflow.Api.Unit.Tests.Mappers.Response.AlphaVantage;

public class GlobalQuoteResponseToGlobalQuoteDomainMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_global_quote_response_to_domain_object(
        GlobalQuote source,
        GlobalQuoteResponseToGlobalQuoteDomainMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(double.Parse(source.High), result.High);
        Assert.Equal(double.Parse(source.Low), result.Low);
        Assert.Equal(double.Parse(source.Open), result.Open);
        Assert.Equal(double.Parse(source.Price), result.Price);
        Assert.Equal(double.Parse(source.Change), result.Change);
        Assert.Equal(source.ChangePerc, result.ChangePerc);
        Assert.Equal(Int32.Parse(source.Volume), result.Volume);
        Assert.Equal(source.Symbol, result.Symbol);
    }
}