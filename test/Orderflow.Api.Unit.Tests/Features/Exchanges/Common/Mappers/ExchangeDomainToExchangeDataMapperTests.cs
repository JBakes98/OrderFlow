using Orderflow.Features.Exchanges.Common.Mappers;
using Orderflow.Features.Exchanges.Common.Models;

namespace Orderflow.Api.Unit.Tests.Features.Exchanges.Common.Mappers;

public class ExchangeDomainToExchangeDataMapperTests
{
    [Fact]
    public void Map_ShouldMapExchangeToExchangeEntityCorrectly()
    {
        // Arrange
        var mapper = new ExchangeDomainToExchangeDataMapper();

        var source = new Exchange(
            Guid.NewGuid(),
            "New York Stock Exchange",
            "NYSE",
            "XNYS",
            "US"
        );

        // Act
        var result = mapper.Map(source);

        // Assert
        Assert.NotNull(result);
        Assert.Equal(source.Id, result.Id);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Abbreviation, result.Abbreviation);
        Assert.Equal(source.Mic, result.Mic);
        Assert.Equal(source.Region, result.Region);
    }
}