using Orderflow.Api.Unit.Tests.Customizations;
using Orderflow.Data.Entities;
using Orderflow.Mappers.Domain;

namespace Orderflow.Api.Unit.Tests.Mappers.Domain;

public class InstrumentDataToInstrumentDomainMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_to_entity_object(
        InstrumentEntity source,
        InstrumentDataToInstrumentDomainMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.ExchangeId, result.ExchangeId);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Sector, result.Sector);
        Assert.Equal(source.Id, result.Id);
    }
}