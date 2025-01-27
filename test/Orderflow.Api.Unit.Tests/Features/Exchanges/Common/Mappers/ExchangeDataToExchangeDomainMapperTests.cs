using Orderflow.Features.Exchanges.Common;
using Orderflow.Features.Exchanges.Common.Mappers;

namespace Orderflow.Api.Unit.Tests.Features.Exchanges.Common.Mappers;

public class ExchangeDataToExchangeDomainMapperTests
{
    [Fact]
    public void Map_ShouldMapExchangeEntityToExchangeCorrectly()
    {
        // Arrange
        var mapper = new ExchangeDataToExchangeDomainMapper();

        var source = new ExchangeEntity(
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