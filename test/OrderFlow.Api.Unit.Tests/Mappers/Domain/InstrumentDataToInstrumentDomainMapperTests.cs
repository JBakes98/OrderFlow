using OrderFlow.Api.Unit.Tests.Customizations;
using OrderFlow.Data.Entities;
using OrderFlow.Mappers.Domain;

namespace OrderFlow.Api.Unit.Tests.Mappers.Domain;

public class InstrumentDataToInstrumentDomainMapperTests
{
    [Theory, AutoMoqData]
    public void Should_map_domain_to_entity_object(
        InstrumentEntity source,
        InstrumentDataToInstrumentDomainMapper sut)
    {
        var result = sut.Map(source);

        Assert.Equal(source.Ticker, result.Ticker);
        Assert.Equal(source.Exchange, result.Exchange);
        Assert.Equal(source.Name, result.Name);
        Assert.Equal(source.Sector, result.Sector);
        Assert.Equal(source.Id, result.Id);
    }
}